
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface
{
    public interface IObjectPool<T>
        where T : MonoBehaviour, IPooledInstance
    {
        public int ActiveObjectsCount { get;}
        public int OverallInstancesGiven { get;}
        public int OverallInstancesCreated { get;}

        public T Get(Vector3 position, Quaternion rotation, Transform parentObject);
    }
}
