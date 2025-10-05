using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using System;
using UnityEngine;
using Zenject;


namespace Assets.CubeRain.CodeBase.Common.Spawners
{
    public class BombSpawner : PooledInstanceSpawner<Bomb>
    {
        private PooledInstanceSpawner<Cube> _cubeSpawner;

        [Inject]
        private void Construct(PooledInstanceSpawner<Cube> cubeSpawner)
        {
            _cubeSpawner = cubeSpawner;
        }

        public override event Action<Bomb> InstanceSpawned;

        private void Start()
        {
            _cubeSpawner.InstanceSpawned += OnCubeSpawned;
        }

        private void OnDestroy()
        {
            _cubeSpawner.InstanceSpawned -= OnCubeSpawned;
        }

        private void OnCubeSpawned(IPooledInstance instance)
        {
            instance.Released += Spawn;
        }

        private void Spawn(IPooledInstance instance)
        {
            if (instance is Cube cube) 
            { 
                Bomb bomb = Pool.Get(cube.transform.position, Quaternion.identity, null);

                InstanceSpawned?.Invoke(bomb);

                cube.Released -= Spawn;
            }
        }
    }
}
