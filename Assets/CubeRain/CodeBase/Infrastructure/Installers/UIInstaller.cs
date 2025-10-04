using Assets.CubeRain.CodeBase.UI.SpawnerStatistics;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.Infrastructure.Installers
{
    public class UIInstaller: MonoInstaller
    {
        [SerializeField] private CubeCounter _cubeCounter;
        [SerializeField] private BombCounter _bombCounter;
        
        public override void InstallBindings()
        {
            RegisterCounters();
            RegisterSpawnerPresenters();
        }

        private void RegisterCounters() 
        {
            Container.Bind<CubeCounter>().FromInstance(_cubeCounter).AsSingle();
            Container.Bind<BombCounter>().FromInstance(_bombCounter).AsSingle();
        }

        private void RegisterSpawnerPresenters() 
        {
            Container.Bind<CubeSpawnerPresenter>().To<CubeSpawnerPresenter>().AsTransient();
            Container.Bind<BombSpawnerPresenter>().To<BombSpawnerPresenter>().AsTransient();
        }
    }
}
