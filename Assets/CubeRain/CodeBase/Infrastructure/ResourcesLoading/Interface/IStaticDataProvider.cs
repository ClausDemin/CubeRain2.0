using Assets.CubeRain.CodeBase.Infrastructure.Configs.Interface;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading.Interface
{
    public interface IStaticDataProvider
    {
        public T GetConfig<T>() where T : ScriptableObject, IConfig;
    }
}
