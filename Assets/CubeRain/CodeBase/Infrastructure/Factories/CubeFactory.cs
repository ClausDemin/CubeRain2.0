using Assets.CubeRain.CodeBase.Common.ColorChangeFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Infrastructure.Configs;
using Assets.CubeRain.CodeBase.Infrastructure.Factories.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading.Interface;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.Infrastructure.Factories
{
    public class CubeFactory : IPooledInstanceFactory<Cube>
    {
        private readonly IInstantiator _instantiator;
        private CubeConfig _config;

        public CubeFactory(IInstantiator instantiator, IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _config = staticDataProvider.GetConfig<CubeConfig>();
        }

        public Cube Create()
        {
            Cube instance = _instantiator.InstantiatePrefabForComponent<Cube>(_config.Prefab);

            instance.Construct(_config.MinLifetime, _config.MaxLifetime);

            MeshRenderer renderer;

            if (instance.TryGetComponent(out renderer))
            {
                renderer.material.color = _config.DefaultColor;
            }

            InitializeColorChanger(instance, renderer);

            return instance;
        }

        public Cube Create(Vector3 position, Quaternion rotation, Transform parentObject)
        {
            Cube instance = Create();

            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = parentObject;

            return instance;
        }

        private void InitializeColorChanger(Cube instance, MeshRenderer renderer)
        {
            if (instance.TryGetComponent(out ColorChanger colorChanger))
            {
                colorChanger.Init(renderer);
            }
        }
    }
}
