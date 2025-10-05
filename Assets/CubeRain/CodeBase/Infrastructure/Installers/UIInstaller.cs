using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.UI.SpawnerStatistics;
using Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View;
using Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View.Interface;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.Infrastructure.Installers
{
    public class UIInstaller: MonoInstaller
    {
        [SerializeField] private PooledInstanceSpawnerView _cubeCounter;
        [SerializeField] private PooledInstanceSpawnerView _bombCounter;
        
        public override void InstallBindings()
        {
            RegisterCounters();
            RegisterSpawnerPresenters();
        }

        private void RegisterCounters() 
        {
            Container.Bind<ICounterView>().To<PooledInstanceSpawnerView>().FromInstance(_cubeCounter).AsCached().WhenInjectedInto<PooledInstanceSpawnerPresenter<Cube>>();
            Container.Bind<ICounterView>().To<PooledInstanceSpawnerView>().FromInstance(_bombCounter).AsCached().WhenInjectedInto<PooledInstanceSpawnerPresenter<Bomb>>();
        }

        private void RegisterSpawnerPresenters() 
        {
            Container.Bind<PooledInstanceSpawnerPresenter<Cube>>().To<CubeSpawnerPresenter>().AsTransient();
            Container.Bind<PooledInstanceSpawnerPresenter<Bomb>>().To<BombSpawnerPresenter>().AsTransient();
        }
    }
}
