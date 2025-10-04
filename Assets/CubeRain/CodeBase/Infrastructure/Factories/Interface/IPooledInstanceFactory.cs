using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.Factories.Interface
{
    public interface IPooledInstanceFactory<T>
        where T : MonoBehaviour, IPooledInstance
    {
        public T Create();
        public T Create(Vector3 position, Quaternion rotation, Transform parentObject);
    }
}