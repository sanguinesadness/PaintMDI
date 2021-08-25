using System;
using System.Drawing;
using PluginSupporter;
using MetadataExtractor;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace ImageTransforms
{
    [Version(1, 1)]
    class Descriptor : IPlugin
    {
        public string Name => "Подробная информация";

        public string Author => "Rustam";

        private Dictionary<string, string> _properties;
        private List<string> _tags = new List<string>() { "Date/Time", "Date/Time Original", "GPS Longitude", "GPS Latitude", "Make", "Model" };
        private string _noInfoFlag = "{unknown}";

        public void Transform(Bitmap bmp)
        {
            _properties = new Dictionary<string, string>();

            DateTime timeNow = DateTime.Now;
            _properties.Add("Date/Time Now", $"{timeNow.Year}:{timeNow.Month}:{timeNow.Day} {timeNow.Hour}:{timeNow.Minute}:{timeNow.Second}");

            AppendGeoData(-1, -1);

            foreach (string tag in _tags)
            {
                _properties.Add(tag, _noInfoFlag);
            }

            ShowData(bmp);
        }

        public void Transform(Bitmap bmp, string filePath)
        {
            _properties = new Dictionary<string, string>();

            DateTime timeNow = DateTime.Now;
            _properties.Add("Date/Time Now", $"{timeNow.Year}:{timeNow.Month}:{timeNow.Day} {timeNow.Hour}:{timeNow.Minute}:{timeNow.Second}");

            string longitudeStr = "0", latitudeStr = "0";

            var directories = ImageMetadataReader.ReadMetadata(filePath);

            foreach (var directory in directories)
            {
                foreach (var tag in directory.Tags)
                {
                    if (_tags.Contains(tag.Name))
                    {
                        _properties.Add(tag.Name, tag.Description);
                    }

                    if (tag.Name == "GPS Longitude")
                        longitudeStr = tag.Description;

                    if (tag.Name == "GPS Latitude")
                        latitudeStr = tag.Description;
                }
            }

            double latitude = ConvertFromDMS(latitudeStr);
            double longitude = ConvertFromDMS(longitudeStr);

            AppendGeoData(latitude, longitude);

            foreach (string tag in _tags)
            {
                if (!_properties.ContainsKey(tag))
                    _properties.Add(tag, _noInfoFlag);
            }

            ShowData(bmp);
        }

        private void ShowData(Bitmap bmp)
        {
            Graphics graphics = Graphics.FromImage(bmp);

            int fontSize = 4;

            if (bmp.Width > 2560)
                fontSize = 18;
            else if (bmp.Width > 1920)
                fontSize = 16;
            else if (bmp.Width > 1800)
                fontSize = 14;
            else if (bmp.Width > 1400)
                fontSize = 12;
            else if (bmp.Width > 1000)
                fontSize = 10;
            else if (bmp.Width > 600)
                fontSize = 8;
            else if (bmp.Width > 200)
                fontSize = 6;

            float gapCoef = 0.05f;

            if (bmp.Height > 600)
                gapCoef = 0.04f;
            else if (bmp.Height > 1000)
                gapCoef = 0.02f;
            else if (bmp.Height > 1400)
                gapCoef = 0.01f;
            else if (bmp.Height > 2000)
                gapCoef = 0.005f;
            else if (bmp.Height > 2500)
                gapCoef = 0.001f;

            int gap = (int)(bmp.Height * gapCoef);

            float coefY = 0.9f;
            float coefX = 0.7f;

            if (bmp.Width < 800)
                coefX = 0.6f;
            else if (bmp.Width < 600)
                coefX = 0.5f;
            else if (bmp.Width < 200)
                coefX = 0.4f;

            if (bmp.Height > bmp.Width)
                coefX -= 0.1f;

            int X = (int)(bmp.Width * coefX);
            int Y = (int)(bmp.Height * coefY);

            Font font = new Font("Consolas", fontSize, FontStyle.Regular);
            Point location = new Point(X, Y);

            foreach (var prop in _properties.Reverse())
            {
                RenderDropshadowText(graphics, $"[{prop.Key}] = {prop.Value}", font, Color.White, Color.Black, 130, location);
                location.Y -= gap;
            }
        }

        private void AppendGeoData(double latitude, double longitude)
        {
            if (latitude < 0 || longitude < 0)
            {
                _properties.Add("GPS Country", _noInfoFlag);
                _properties.Add("GPS State", _noInfoFlag);
                _properties.Add("GPS Suburb", _noInfoFlag);
                _properties.Add("GPS Road", _noInfoFlag);
                _properties.Add("GPS City", _noInfoFlag);
                _properties.Add("GPS Postcode", _noInfoFlag);

                return;
            }

            RootObject rootObject = GetAddress(latitude, longitude);
            _properties.Add("GPS Country", !string.IsNullOrEmpty(rootObject.address.country) ? rootObject.address.country : _noInfoFlag);
            _properties.Add("GPS State", !string.IsNullOrEmpty(rootObject.address.state) ? rootObject.address.state : _noInfoFlag);
            _properties.Add("GPS Suburb", !string.IsNullOrEmpty(rootObject.address.suburb) ? rootObject.address.suburb : _noInfoFlag);
            _properties.Add("GPS Road", !string.IsNullOrEmpty(rootObject.address.road) ? rootObject.address.road : _noInfoFlag);
            _properties.Add("GPS City", !string.IsNullOrEmpty(rootObject.address.city) ? rootObject.address.city : _noInfoFlag);
            _properties.Add("GPS Postcode", !string.IsNullOrEmpty(rootObject.address.postcode) ? rootObject.address.postcode : _noInfoFlag);
        }

        private void RenderDropshadowText(Graphics graphics, string text, Font font, Color foreground, 
                                          Color shadow, int shadowAlpha, PointF location)
        {
            const int DISTANCE = 2;
            for (int offset = 1; 0 <= offset; offset--)
            {
                Color color = ((offset < 1) ?
                    foreground : Color.FromArgb(shadowAlpha, shadow));
                using (var brush = new SolidBrush(color))
                {
                    var point = new PointF()
                    {
                        X = location.X + (offset * DISTANCE),
                        Y = location.Y + (offset * DISTANCE)
                    };
                    graphics.DrawString(text, font, brush, point);
                }
            }
        }

        private static RootObject GetAddress(double latitude, double longitude)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");
            var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + latitude + "&lon=" + longitude);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
            RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));
            return rootObject;
        }

        private double ConvertFromDMS(string coordinate)
        {
            string degreeStr = Regex.Match(coordinate, @"(\d*[.]?\d*)°").Groups[1].Value;
            string minuteStr = Regex.Match(coordinate, @"(\d*[.]?\d*)'").Groups[1].Value;
            string secStr = Regex.Match(coordinate, @"(\d*[.]?\d*)""").Groups[1].Value;

            if (string.IsNullOrEmpty(degreeStr) || string.IsNullOrEmpty(minuteStr) || string.IsNullOrEmpty(secStr))
            {
                return -1;
            }

            double degree = double.Parse(degreeStr);
            double minute = double.Parse(minuteStr);
            double sec = double.Parse(secStr);

            double result = degree + ((minute + (sec / 60)) / 60);
            return result;
        }
    }

    [DataContract]
    internal class Address
    {
        [DataMember]
        public string road { get; set; }
        [DataMember]
        public string suburb { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string state_district { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string postcode { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string country_code { get; set; }
    }

    [DataContract]
    internal class RootObject
    {
        [DataMember]
        public string place_id { get; set; }
        [DataMember]
        public string licence { get; set; }
        [DataMember]
        public string osm_type { get; set; }
        [DataMember]
        public string osm_id { get; set; }
        [DataMember]
        public string lat { get; set; }
        [DataMember]
        public string lon { get; set; }
        [DataMember]
        public string display_name { get; set; }
        [DataMember]
        public Address address { get; set; }
    }
}
