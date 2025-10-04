using Assets.CubeRain.CodeBase.Infrastructure.Installers.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading.Interface;
using Zenject;

namespace Assets.CubeRain.CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            RegisterStaticDataProvider();
            RegisterInterfaces();
        }

        private void RegisterStaticDataProvider()
        {
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle();
        }

        private void RegisterInterfaces() 
        { 
            Container.BindInterfacesTo<BootstrapInstaller>().AsSingle();
        }
    }
}

