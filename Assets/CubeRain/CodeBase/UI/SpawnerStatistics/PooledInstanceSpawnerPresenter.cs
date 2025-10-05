using Assets.CubeRain.CodeBase.Common.Spawners;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View.Interface;
using System;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics
{
    public class PooledInstanceSpawnerPresenter<T>: IDisposable
        where T : MonoBehaviour, IPooledInstance
    {
        private ICounterView _counter;
        private PooledInstanceSpawner<T> _spawner;

        [Inject]
        public PooledInstanceSpawnerPresenter(ICounterView counterView, PooledInstanceSpawner<T> spawner)
        {
            _counter = counterView;
            _spawner = spawner;

            _spawner.InstanceSpawned += OnInstanceCreated;
        }

        public void Dispose()
        {
            _spawner.InstanceSpawned -= OnInstanceCreated;
        }

        protected virtual void OnInstanceCreated(IPooledInstance instance)
        {
            instance.Released += OnInstanceReleased;
            instance.Disposed -= OnInstanceDisposed;

            _counter.UpdateOverallCount(_spawner.InstantiatedObjectsCount.ToString());
            _counter.UpdateCreatedCount(_spawner.CreatedObjectsCount.ToString());
            _counter.UpdateActiveCount(_spawner.ActiveObjectsCount.ToString());
        }

        private void OnInstanceReleased(IPooledInstance instance)
        {
            _counter.UpdateActiveCount(_spawner.ActiveObjectsCount.ToString());
        }

        private void OnInstanceDisposed(IPooledInstance instance)
        {
            instance.Released -= OnInstanceReleased;
            instance.Disposed -= OnInstanceDisposed;
        }
    }
}
