using System;

namespace PluginSupporter
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public int Major { get; private set; }

        public int Minor { get; private set; }

        public VersionAttribute(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
    }
}
