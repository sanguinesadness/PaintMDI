using System;
using System.Drawing;

namespace PluginSupporter
{
    public interface IPlugin
    {
        string Name { get; }

        string Author { get; }

        void Transform(Bitmap bmp);

        void Transform(Bitmap bmp, string filePath);
    }
}
