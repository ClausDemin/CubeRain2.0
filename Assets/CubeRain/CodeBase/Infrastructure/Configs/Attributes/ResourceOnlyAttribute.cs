using System;

namespace Assets.CubeRain.CodeBase.Infrastructure.Configs.Attributes
{
    public class ResourceOnlyAttribute : Attribute
    {
        private const string ResourcesPath = "Assets/Resources";

        public bool Validate(string path) 
        {
            return path.StartsWith(ResourcesPath);
        }
    }
}
