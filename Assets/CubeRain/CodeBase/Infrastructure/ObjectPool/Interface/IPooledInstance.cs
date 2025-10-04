using System;

namespace Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface
{
    public interface IPooledInstance
    {
        public event Action<IPooledInstance> Released;
        public event Action<IPooledInstance> Disposed;

        public void Reset();
        public void Enable();
        public void Disable();
    }
}