using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PaintMDI_WF
{
    enum CursorTypes
    {
        Brush,
        Grabber,
        Eraser,
        Default
    }

    public partial class Canvas : Form
    {
        // данные для реализации перемещения окна
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private Bitmap _bitmap;
        private Point _fromPosition;
        private ImageFormat _fileFormat = null;

        private bool _lineShapeSelected = false;
        private bool _rectangleShapeSelected = false;
        private bool _ellipseShapeSelected = false;
        private bool _starShapeSelected = false;
        private Point _shapeStartPoint;
        private Bitmap _tmpBitmap;

        public Graphics Graphics { get; private set; }

        public string FilePath { get; private set; } = string.Empty;

        public Canvas()
        {
            InitializeComponent();

            Bitmap = new Bitmap(this.Width, this.Height);
            pictureBox.Image = Bitmap;

            this.Resize += Canvas_Resize;
        }

        public Canvas(string filePath)
        {
            InitializeComponent();

            string ex = Path.GetExtension(filePath);
            if (ex != ".jpg" && ex != ".jpeg" && ex != ".bmp")
            {
                Bitmap = new Bitmap(this.Width, this.Height);
                pictureBox.Image = _bitmap;

                MessageBox.Show("Unable to load this type of file.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                Bitmap = new Bitmap(fileStream);
                Graphics = Graphics.FromImage(Bitmap);

                pictureBox.Width = Bitmap.Width;
                pictureBox.Height = Bitmap.Height;
                pictureBox.Image = Bitmap;

                FilePath = filePath;

                if (ex == ".jpg" || ex == ".jpeg")
                    _fileFormat = ImageFormat.Jpeg;
                else if (ex == "bmp")
                    _fileFormat = ImageFormat.Bmp;
            }

            this.Text = Path.GetFileName(FilePath);
            this.Resize += Canvas_Resize;
        }

        public Bitmap Bitmap
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                pictureBox.Image = _bitmap;
                this.Width = _bitmap.Width;
                this.Height = _bitmap.Height;
            }
        }

        private bool IsAnyShapeSelected()
        {
            MainForm parent = (ParentForm as MainForm);

            _lineShapeSelected = parent.IsToolSelected(Tools.LineShape);
            _rectangleShapeSelected = parent.IsToolSelected(Tools.RectangleShape);
            _ellipseShapeSelected = parent.IsToolSelected(Tools.EllipseShape);
            _starShapeSelected = parent.IsToolSelected(Tools.StarShape);

            return _lineShapeSelected || _rectangleShapeSelected || _ellipseShapeSelected || _starShapeSelected;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _fromPosition = e.Location;

            if (e.Button == MouseButtons.Left && (ParentForm as MainForm).IsToolSelected(Tools.Grabber))
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

            if (IsAnyShapeSelected())
            {
                _shapeStartPoint = e.Location;
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsAnyShapeSelected())
            {
                Bitmap = new Bitmap(_tmpBitmap);
                pictureBox.Image = Bitmap;
            }

            if (_lineShapeSelected)
            {
                _lineShapeSelected = false;
            }
            else if (_rectangleShapeSelected)
            {
                _rectangleShapeSelected = false;
            }
            else if (_ellipseShapeSelected)
            {
                _ellipseShapeSelected = false;
            }
            else if (_starShapeSelected)
            {
                _starShapeSelected = false;
            }
        }

        private void DrawLine(Point destination)
        {
            MainForm parent = ParentForm as MainForm;

            _tmpBitmap = new Bitmap(Bitmap);
            pictureBox.Image = _tmpBitmap;

            Graphics = Graphics.FromImage(_tmpBitmap);
            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Pen pen = new Pen(parent.BrushColor, parent.BrushWidth);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            Graphics.DrawLine(pen, _shapeStartPoint, destination);

            pictureBox.Invalidate();
            Refresh();
        }

        private void DrawRectangle(Point endPoint)
        {
            MainForm parent = ParentForm as MainForm;

            _tmpBitmap = new Bitmap(Bitmap);
            pictureBox.Image = _tmpBitmap;

            Graphics = Graphics.FromImage(_tmpBitmap);
            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Pen pen = new Pen(parent.BrushColor, parent.BrushWidth);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            var rc = new Rectangle(
                Math.Min(_shapeStartPoint.X, endPoint.X),
                Math.Min(_shapeStartPoint.Y, endPoint.Y),
                Math.Abs(endPoint.X - _shapeStartPoint.X),
                Math.Abs(endPoint.Y - _shapeStartPoint.Y));

            if (parent.RectangleFilled)
                Graphics.FillRectangle(new SolidBrush(pen.Color), rc);

            Graphics.DrawRectangle(pen, rc);

            pictureBox.Invalidate();
            Refresh();
        }

        private void DrawEllipse(Point endPoint)
        {
            MainForm parent = ParentForm as MainForm;

            _tmpBitmap = new Bitmap(Bitmap);
            pictureBox.Image = _tmpBitmap;

            Graphics = Graphics.FromImage(_tmpBitmap);
            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Pen pen = new Pen(parent.BrushColor, parent.BrushWidth);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            var rc = new Rectangle(
                Math.Min(_shapeStartPoint.X, endPoint.X),
                Math.Min(_shapeStartPoint.Y, endPoint.Y),
                Math.Abs(endPoint.X - _shapeStartPoint.X),
                Math.Abs(endPoint.Y - _shapeStartPoint.Y));

            if (parent.EllipseFilled)
                Graphics.FillEllipse(new SolidBrush(pen.Color), rc);

            Graphics.DrawEllipse(pen, rc);

            pictureBox.Invalidate();
            Refresh();
        }

        private void DrawStar(Point endPoint)
        {
            MainForm parent = ParentForm as MainForm;

            _tmpBitmap = new Bitmap(Bitmap);
            pictureBox.Image = _tmpBitmap;

            Graphics = Graphics.FromImage(_tmpBitmap);
            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Pen pen = new Pen(parent.BrushColor, parent.BrushWidth);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            var rc = new Rectangle(
                Math.Min(_shapeStartPoint.X, endPoint.X),
                Math.Min(_shapeStartPoint.Y, endPoint.Y),
                Math.Abs(endPoint.X - _shapeStartPoint.X),
                Math.Abs(endPoint.Y - _shapeStartPoint.Y));

            DrawShape(Graphics, pen, 5, 4, rc, parent.StarFilled);

            pictureBox.Invalidate();
            Refresh();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            MainForm parent = ParentForm as MainForm;

            if (_lineShapeSelected)
            {
                DrawLine(e.Location);
                return;
            }
            else if (_rectangleShapeSelected)
            {
                DrawRectangle(e.Location);
                return;
            }
            else if (_ellipseShapeSelected)
            {
                DrawEllipse(e.Location);
                return;
            }
            else if (_starShapeSelected)
            {
                DrawStar(e.Location);
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                Graphics = Graphics.FromImage(Bitmap);
                Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                Pen pen;
                if (parent.IsToolSelected(Tools.Eraser))
                {
                    pen = new Pen(pictureBox.BackColor, parent.BrushWidth);
                }
                else
                {
                    pen = new Pen(parent.BrushColor, parent.BrushWidth);
                }
                pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                Graphics.DrawLine(pen, _fromPosition, e.Location);
                _fromPosition = e.Location;

                pictureBox.Invalidate();
                Refresh();
            }

            parent.SetPositions(e.X, e.Y);
        }

        private void Canvas_Resize(object sender, EventArgs e)
        {
            (ParentForm as MainForm).SetCanvasSize(this.Width, this.Height);
        }

        private void Canvas_Activated(object sender, EventArgs e)
        {
            (ParentForm as MainForm).SetCanvasSize(this.Width, this.Height);
        }

        private void SetCursor(CursorTypes cursorType)
        {
            switch (cursorType)
            {
                case CursorTypes.Brush:
                    try { Cursor = new Cursor(Properties.Resources.brushCursor.GetHicon()); }
                    catch { }
                    break;
                case CursorTypes.Grabber:
                    try { Cursor = new Cursor(Properties.Resources.grabCursor.GetHicon()); }
                    catch { }
                    break;
                case CursorTypes.Eraser:
                    try { Cursor = new Cursor(Properties.Resources.eraserCursor.GetHicon()); }
                    catch { }
                    break;
                case CursorTypes.Default:
                    try { Cursor = Cursors.Default; }
                    catch { }
                    break;
            }
        }

        public void ClearCanvas()
        {
            Bitmap = new Bitmap(this.Width, this.Height);
            pictureBox.Image = Bitmap;
        }

        public void SaveAs()
        {
            Bitmap blank = new Bitmap(Bitmap.Width, Bitmap.Height);
            Graphics = Graphics.FromImage(blank);
            Graphics.Clear(pictureBox.BackColor);
            Graphics.DrawImage(Bitmap, 0, 0, Bitmap.Width, Bitmap.Height);

            Bitmap tempImage = new Bitmap(blank);
            blank.Dispose();
            Bitmap.Dispose();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.AddExtension = true;
            saveDialog.FileName = this.Text;
            saveDialog.Filter = "Файлы JPEG (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp";

            ImageFormat[] ImgFormats = { ImageFormat.Bmp, ImageFormat.Jpeg };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = saveDialog.FileName;
                _fileFormat = ImgFormats[saveDialog.FilterIndex - 1];

                using (FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    tempImage.Save(fileStream, _fileFormat);
                }
            }

            Bitmap = new Bitmap(tempImage);

            tempImage.Dispose();
        }

        public void SetCanvasBackColor(Color color)
        {
            pictureBox.BackColor = color;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                SaveAs();
                return;
            }

            using (FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Bitmap blank = new Bitmap(Bitmap.Width, Bitmap.Height);
                Graphics = Graphics.FromImage(blank);
                Graphics.Clear(pictureBox.BackColor);
                Graphics.DrawImage(Bitmap, 0, 0, Bitmap.Width, Bitmap.Height);

                Bitmap tempImage = new Bitmap(blank);
                blank.Dispose();
                Bitmap.Dispose();

                tempImage.Save(fileStream, _fileFormat);

                Bitmap = new Bitmap(tempImage);

                tempImage.Dispose();
            }
        }

        public void ResizeWindow(float width, float height)
        {
            if (width < 100)
                width = 100;
            if (height < 100)
                height = 100;

            Size newSize = new Size((int)width, (int)height);
            Bitmap bmp = new Bitmap(Bitmap, newSize);

            Bitmap = bmp;
        }

        private void HandleCursor()
        {
            MainForm parent = ParentForm as MainForm;

            if (parent.IsToolSelected(Tools.Grabber))
            {
                SetCursor(CursorTypes.Grabber);
                return;
            }
            else if (parent.IsToolSelected(Tools.Eraser))
            {
                SetCursor(CursorTypes.Eraser);
            }
            else
            {
                SetCursor(CursorTypes.Brush);
            }
        }

        #region Методы для отрисовки звезды (http://csharphelper.com/blog/2014/08/draw-a-star-with-a-given-number-of-points-in-c/)

        // Generate the points for a star.
        private PointF[] MakeStarPoints(double start_theta, int num_points, int skip, Rectangle rect)
        {
            double theta, dtheta;
            PointF[] result;
            float rx = rect.Width / 2f;
            float ry = rect.Height / 2f;
            float cx = rect.X + rx;
            float cy = rect.Y + ry;

            // If this is a polygon, don't bother with concave points.
            if (skip == 1)
            {
                result = new PointF[num_points];
                theta = start_theta;
                dtheta = 2 * Math.PI / num_points;
                for (int i = 0; i < num_points; i++)
                {
                    result[i] = new PointF(
                        (float)(cx + rx * Math.Cos(theta)),
                        (float)(cy + ry * Math.Sin(theta)));
                    theta += dtheta;
                }
                return result;
            }

            // Find the radius for the concave vertices.
            double concave_radius =
                CalculateConcaveRadius(num_points, skip);

            // Make the points.
            result = new PointF[2 * num_points];
            theta = start_theta;
            dtheta = Math.PI / num_points;
            for (int i = 0; i < num_points; i++)
            {
                result[2 * i] = new PointF(
                    (float)(cx + rx * Math.Cos(theta)),
                    (float)(cy + ry * Math.Sin(theta)));
                theta += dtheta;
                result[2 * i + 1] = new PointF(
                    (float)(cx + rx * Math.Cos(theta) * concave_radius),
                    (float)(cy + ry * Math.Sin(theta) * concave_radius));
                theta += dtheta;
            }
            return result;
        }

        // Calculate the inner star radius.
        private double CalculateConcaveRadius(int num_points, int skip)
        {
            // For really small numbers of points.
            if (num_points < 5) return 0.33f;

            // Calculate angles to key points.
            double dtheta = 2 * Math.PI / num_points;
            double theta00 = -Math.PI / 2;
            double theta01 = theta00 + dtheta * skip;
            double theta10 = theta00 + dtheta;
            double theta11 = theta10 - dtheta * skip;

            // Find the key points.
            PointF pt00 = new PointF(
                (float)Math.Cos(theta00),
                (float)Math.Sin(theta00));
            PointF pt01 = new PointF(
                (float)Math.Cos(theta01),
                (float)Math.Sin(theta01));
            PointF pt10 = new PointF(
                (float)Math.Cos(theta10),
                (float)Math.Sin(theta10));
            PointF pt11 = new PointF(
                (float)Math.Cos(theta11),
                (float)Math.Sin(theta11));

            // See where the segments connecting the points intersect.
            bool lines_intersect, segments_intersect;
            PointF intersection, close_p1, close_p2;
            FindIntersection(pt00, pt01, pt10, pt11,
                out lines_intersect, out segments_intersect,
                out intersection, out close_p1, out close_p2);

            // Calculate the distance between the
            // point of intersection and the center.
            return Math.Sqrt(
                intersection.X * intersection.X +
                intersection.Y * intersection.Y);
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        private void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointF intersection,
            out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        private void DrawShape(Graphics gr, Pen the_pen, int num_points, int skip, Rectangle rect, bool fill)
        {
            // Get the star's points.
            PointF[] star_points =
                MakeStarPoints(-Math.PI / 2, num_points, skip, rect);

            // Draw the star.
            if (fill)
                gr.FillPolygon(new SolidBrush((ParentForm as MainForm).BrushColor), star_points);
            gr.DrawPolygon(the_pen, star_points);
        }

        #endregion

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            HandleCursor();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            (ParentForm as MainForm).ClearPositions();

            SetCursor(CursorTypes.Default);
        }
    }
}
