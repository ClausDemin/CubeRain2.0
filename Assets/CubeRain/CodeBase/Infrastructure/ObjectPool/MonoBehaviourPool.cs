using Assets.CubeRain.CodeBase.Infrastructure.Factories.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.ObjectPool
{
    public class MonoBehaviourPool<T> : IObjectPool<T>
        where T: MonoBehaviour, IPooledInstance
    {
        private readonly IPooledInstanceFactory<T> _factory;
        private readonly Queue<T> _items = new();

        public MonoBehaviourPool(IPooledInstanceFactory<T> factory, int prewarmedCount = 0)
        {
            _factory = factory;

            CreatePrewarmedItems(prewarmedCount);
        }

        public int ActiveObjectsCount { get; private set; }
        public int OverallInstancesGiven { get; private set; }
        public int OverallInstancesCreated { get; private set; }

        public T Get(Vector3 position, Quaternion rotation, Transform parentObject)
        {
            T instance;

            if (_items.TryDequeue(out T freeItem))
            {
                instance = freeItem;
            }
            else
            {
                instance = Instantiate();
                instance.Disable();

                OverallInstancesCreated++;
            }

            PrepareInstance(instance, position, rotation, parentObject);

            OverallInstancesGiven++;
            ActiveObjectsCount++;

            return instance;
        }

        private void PrepareInstance(T instance, Vector3 position, Quaternion rotation, Transform parentObject)
        {
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = parentObject;

            instance.Enable();
        }

        private T Instantiate() 
        {
            T instance = _factory.Create();

            instance.Released += OnRelease;
            instance.Disposed += OnDispose;

            return instance;
        }

        private void CreatePrewarmedItems(int prewarmedCount) 
        {
            for (int i = 0; i < prewarmedCount; i++) 
            { 
                _items.Enqueue(Instantiate());
            }
        }

        private void OnRelease(IPooledInstance instance) 
        { 
            instance.Disable();
            instance.Reset();

            _items.Enqueue((instance) as T);

            ActiveObjectsCount--;
        }

        private void OnDispose(IPooledInstance instance) 
        {
            instance.Released -= OnRelease;
            instance.Disposed -= OnDispose;
        }
    }
}
