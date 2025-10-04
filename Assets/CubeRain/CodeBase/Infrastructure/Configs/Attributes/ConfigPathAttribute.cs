using System;

namespace Assets.CubeRain.CodeBase.Infrastructure.Configs.Attributes
{
    public class ConfigPathAttribute : Attribute
    {
        public string Path { get; }

        public ConfigPathAttribute(string path)
        {
            Path = path;
        }
    }
}
