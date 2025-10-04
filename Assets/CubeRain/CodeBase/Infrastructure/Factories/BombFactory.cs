using Assets.CubeRain.CodeBase.Common;
using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.ColorChangeFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Common.ExplosionFeature;
using Assets.CubeRain.CodeBase.Infrastructure.Configs;
using Assets.CubeRain.CodeBase.Infrastructure.Factories.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading.Interface;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.Infrastructure.Factories
{
    internal class BombFactory : IPooledInstanceFactory<Bomb>
    {
        private readonly IInstantiator _instantiator;
        private readonly BombConfig _config;

        public BombFactory(IInstantiator instantiator, IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _config = staticDataProvider.GetConfig<BombConfig>();
        }

        public Bomb Create()
        {
            Bomb instance = _instantiator.InstantiatePrefabForComponent<Bomb>(_config.Prefab);

            MeshRenderer renderer;

            if (instance.TryGetComponent(out renderer)) 
            {
                InitializeColorChanger(instance, renderer);
            }

            InitializeExploder(instance);

            return instance;
        }

        public Bomb Create(Vector3 position, Quaternion rotation, Transform parentObject)
        {
            Bomb instance = Create();

            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = parentObject;

            return instance;
        }

        private void InitializeColorChanger(Bomb instance, MeshRenderer renderer)
        {
            if (instance.TryGetComponent(out ColorChanger colorChanger))
            {
                colorChanger.Init(renderer);
            }
        }

        private void InitializeExploder(Bomb instance) 
        {
            if (instance.TryGetComponent(out Exploder exploder)) 
            {
                exploder.Init(_config.ExplosionForce, _config.ExplosionRadius);
            }
        }
    }
}
