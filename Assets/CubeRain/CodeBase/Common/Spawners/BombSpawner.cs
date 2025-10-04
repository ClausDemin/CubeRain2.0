using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Infrastructure.Configs;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading.Interface;
using System;
using UnityEngine;
using Zenject;


namespace Assets.CubeRain.CodeBase.Common.Spawners
{
    public class BombSpawner : PooledInstanceSpawner<Bomb>
    {
        private CubeSpawner _cubeSpawner;

        [Inject]
        private void Construct(CubeSpawner cubeSpawner)
        {
            _cubeSpawner = cubeSpawner;
        }

        public event Action<Bomb> BombSpawned;

        private void Start()
        {
            _cubeSpawner.CubeSpawned += OnCubeSpawned;
        }

        private void OnDestroy()
        {
            _cubeSpawner.CubeSpawned -= OnCubeSpawned;
        }

        private Vector3 ComputeSpawnPosition()
        {
            return transform.position;
        }

        private void OnCubeSpawned(Cube cube)
        {
            cube.Released += Spawn;
        }

        private void Spawn(IPooledInstance instance)
        {
            if (instance is Cube cube) 
            { 
                Bomb bomb = Pool.Get(cube.transform.position, Quaternion.identity, null);

                BombSpawned?.Invoke(bomb);

                cube.Released -= Spawn;
            }
        }
    }
}
