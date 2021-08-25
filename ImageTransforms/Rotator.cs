using System;
using System.Drawing;
using PluginSupporter;

namespace ImageTransforms
{
    [Version(1, 0)]
    class Rotator : IPlugin
    {
        public string Name => "Поворот на 90° вправо";

        public string Author => "Rustam";

        public void Transform(Bitmap bmp)
        {
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        public void Transform(Bitmap bmp, string filePath)
        {
            Transform(bmp);
        }
    }
}
