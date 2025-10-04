using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.Common.Spawners
{
    public abstract class PooledInstanceSpawner<T> : MonoBehaviour
        where T : MonoBehaviour, IPooledInstance
    {
        protected IObjectPool<T> Pool;

        [Inject]
        private void Construct(IObjectPool<T> pool) 
        {
            Pool = pool;
        }

        public int InstantiatedObjectsCount => Pool.OverallInstancesGiven;
        public int CreatedObjectsCount => Pool.OverallInstancesCreated;
        public int ActiveObjectsCount => Pool.ActiveObjectsCount;
    }
}
