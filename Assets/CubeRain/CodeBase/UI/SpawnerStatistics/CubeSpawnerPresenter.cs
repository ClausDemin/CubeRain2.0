using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Common.Spawners;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class CubeSpawnerPresenter
    {
        private CubeCounter _counter;
        private CubeSpawner _spawner;

        [Inject]
        public CubeSpawnerPresenter(CubeCounter counter, CubeSpawner spawner)
        {
            _counter = counter;
            _spawner = spawner;

            _spawner.CubeSpawned += OnCubeCreated;
        }

        private void OnCubeCreated(Cube instance) 
        {
            instance.Released += OnCubeReleased;

            _counter.UpdateOverallCubesCount(_spawner.InstantiatedObjectsCount.ToString());
            _counter.UpdateCreatedCubesCount(_spawner.CreatedObjectsCount.ToString());
            _counter.UpdateActiveCubesCount(_spawner.ActiveObjectsCount.ToString());

        }

        private void OnCubeReleased(IPooledInstance instance) 
        {
            _counter.UpdateActiveCubesCount(_spawner.ActiveObjectsCount.ToString());
        }
    }
}
