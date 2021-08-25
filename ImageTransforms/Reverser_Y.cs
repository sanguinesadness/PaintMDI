using System;
using System.Drawing;
using PluginSupporter;

namespace ImageTransforms
{
    [Version(1, 0)]
    class Reverser_Y : IPlugin
    {
        public string Name => "Переворот по вертикали";

        public string Author => "Rustam";

        public void Transform(Bitmap bmp)
        {
            bmp.RotateFlip(RotateFlipType.Rotate180FlipY);
        }

        public void Transform(Bitmap bmp, string filePath)
        {
            Transform(bmp);
        }
    }
}
