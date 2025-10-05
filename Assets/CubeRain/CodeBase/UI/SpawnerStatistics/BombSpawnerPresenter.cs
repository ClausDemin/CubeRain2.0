using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.Spawners;
using Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View.Interface;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class BombSpawnerPresenter : PooledInstanceSpawnerPresenter<Bomb>
    {
        [Inject]
        public BombSpawnerPresenter(ICounterView counterView, PooledInstanceSpawner<Bomb> spawner) : base(counterView, spawner)
        {
        }
    }
}
