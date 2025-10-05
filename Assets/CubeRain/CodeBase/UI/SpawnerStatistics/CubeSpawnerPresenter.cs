using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Common.Spawners;
using Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View.Interface;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class CubeSpawnerPresenter : PooledInstanceSpawnerPresenter<Cube>
    {
        [Inject]
        public CubeSpawnerPresenter(ICounterView counterView, PooledInstanceSpawner<Cube> spawner) : base(counterView, spawner)
        {
        }
    }
}
