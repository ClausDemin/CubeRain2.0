using Assets.CubeRain.CodeBase.Infrastructure.Configs.Attributes;
using Assets.CubeRain.CodeBase.Infrastructure.Configs.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading
{
    public class StaticDataProvider: IStaticDataProvider
    {
        private ConfigPathProvider _configPathProvider;
        private Dictionary<Type, IConfig> _configs;

        public StaticDataProvider() 
        {
            _configPathProvider = Resources.Load<ConfigPathProvider>(GetConfigProviderPath());
            _configs = new();

            LoadConfigs();
        }

        public T GetConfig<T>()
            where T: ScriptableObject, IConfig
        {
            Type type = typeof(T);

            if (_configs.TryGetValue(type, out IConfig config)) 
            {
                return config as T;
            }

            throw new KeyNotFoundException($"Config with type {type} cannot be loaded");
        }

        private void LoadConfigs() 
        {
            foreach (Type configType in _configPathProvider.RegisteredTypes) 
            {
                string path = _configPathProvider.GetPath(configType);

                IConfig config = (IConfig) Resources.Load(path, configType);

                _configs.Add(configType, config);
            }
        }

        private string GetConfigProviderPath() 
        {
            Type type = typeof(ConfigPathProvider);

            if (type.IsDefined(typeof(ConfigPathAttribute), false)) 
            {
                ConfigPathAttribute configPath = type.GetCustomAttribute<ConfigPathAttribute>();

                return configPath.Path;
            }

            return null;
        }
    }
}
