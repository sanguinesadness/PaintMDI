using System;
using System.Drawing;
using PluginSupporter;

namespace ImageTransforms
{
    [Version(1, 0)]
    class Reverser_X : IPlugin
    {
        public string Name => "Переворот по горизонтали";

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
