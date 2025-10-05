using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Common.Spawners;
using Assets.CubeRain.CodeBase.Infrastructure.Factories;
using Assets.CubeRain.CodeBase.Infrastructure.Factories.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.Infrastructure.Installers
{
    public class SceneInstaller: MonoInstaller
    {
        [SerializeField] private CubeSpawner _cubeSpawner;
        [SerializeField] private BombSpawner _bombSpawner;

        public override void InstallBindings()
        {
            RegisterFactories();
            RegisterObjectPools();
            RegisterSpawners();
        }
        
        private void RegisterFactories()
        {
            Container.Bind<IPooledInstanceFactory<Bomb>>().To<BombFactory>().AsSingle();
            Container.Bind<IPooledInstanceFactory<Cube>>().To<CubeFactory>().AsSingle();
        }

        private void RegisterObjectPools()
        {
            Container.Bind<IObjectPool<Bomb>>().To<MonoBehaviourPool<Bomb>>().AsTransient();
            Container.Bind<IObjectPool<Cube>>().To<MonoBehaviourPool<Cube>>().AsTransient();
        }

        private void RegisterSpawners() 
        { 
            Container.Bind<PooledInstanceSpawner<Cube>>().To<CubeSpawner>().FromInstance(_cubeSpawner).AsSingle();
            Container.Bind<PooledInstanceSpawner<Bomb>>().To<BombSpawner>().FromInstance(_bombSpawner).AsSingle();
        }
    }
}
