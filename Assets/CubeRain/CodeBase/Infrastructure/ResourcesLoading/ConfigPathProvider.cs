using Assets.CubeRain.CodeBase.Infrastructure.Configs.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading
{
    [ResourceOnly]
    [ConfigPath("ConfigProvider")]
    [CreateAssetMenu(menuName = "Configs/ConfigPathProvider", fileName = "ConfigProvider")]
    public class ConfigPathProvider : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<string> _paths = new List<string>();
        [SerializeField] private List<string> _typeNames = new List<string>();

        private List<Type> _registeredTypes = new List<Type>();
        private Dictionary<Type, string> _typePathPairs = new Dictionary<Type, string>();

        public IEnumerable<Type> RegisteredTypes => _registeredTypes;

        public string GetPath<T>()
        {
            Type type = typeof(T);

            if (_typePathPairs.TryGetValue(type, out string path))
            {
                return path;
            }

            throw new KeyNotFoundException($"config with type {type} not registered");
        }

        public void SetPaths(List<string> paths)
        {
            if (paths != null)
            {
                _paths = paths;
                _registeredTypes.Clear();
                _typePathPairs.Clear();
            }
        }

        public void SetTypeNames(List<string> typeNames)
        {
            if (typeNames != null)
            {
                _typeNames = typeNames;
            }
        }

        public void OnBeforeSerialize()
        {
            if (_typePathPairs.Count > 0)
            {
                _paths.Clear();
                _typeNames.Clear();
                _registeredTypes.Clear();

                foreach (KeyValuePair<Type, string> pair in _typePathPairs)
                {
                    _registeredTypes.Add(pair.Key);
                    _typeNames.Add(pair.Key.FullName);
                    _paths.Add(pair.Value);
                }
            }
        }

        public void OnAfterDeserialize()
        {
            if (_paths.Count == _typeNames.Count)
            {
                DeserializeTypePathPairs(GetTypesFromNames());
            }
        }

        private void DeserializeTypePathPairs(List<Type> types)
        {
            if (types.Count == _paths.Count)
            {
                _typePathPairs.Clear();

                for (int i = 0; i < _paths.Count; i++)
                {
                    if (_typePathPairs.ContainsKey(types[i]) == false)
                    {
                        _registeredTypes.Add(types[i]);
                        _typePathPairs.Add(types[i], _paths[i]);
                    }
                }
            }
        }

        private List<Type> GetTypesFromNames()
        {
            List<Type> types = new List<Type>();

            foreach (string name in _typeNames)
            {
                types.Add(Type.GetType(name));
            }

            return types;
        }

        public string GetPath(Type type)
        {
            if (_typePathPairs.TryGetValue(type, out string path))
            {
                return path;
            }

            throw new KeyNotFoundException($"config with type {type} not registered");
        }
    }
}
