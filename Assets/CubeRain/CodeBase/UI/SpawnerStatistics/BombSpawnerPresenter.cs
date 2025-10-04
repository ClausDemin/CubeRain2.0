using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Common.Spawners;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class BombSpawnerPresenter
    {
        private BombCounter _counter;
        private BombSpawner _spawner;

        [Inject]
        public BombSpawnerPresenter(BombCounter counter, BombSpawner spawner)
        {
            _counter = counter;
            _spawner = spawner;

            _spawner.BombSpawned += OnBombCreated;
        }

        private void OnBombCreated(Bomb instance)
        {
            instance.Released += OnCubeReleased;

            _counter.UpdateOverallBombsCount(_spawner.InstantiatedObjectsCount.ToString());
            _counter.UpdateCreatedBombsCount(_spawner.CreatedObjectsCount.ToString());
            _counter.UpdateActiveBombsCount(_spawner.ActiveObjectsCount.ToString());

        }

        private void OnCubeReleased(IPooledInstance instance)
        {
            _counter.UpdateActiveBombsCount(_spawner.ActiveObjectsCount.ToString());
        }
    }
}
