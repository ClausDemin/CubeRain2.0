using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Infrastructure.Configs;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading.Interface;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.CubeRain.CodeBase.Common.Spawners
{
    public class CubeSpawner: PooledInstanceSpawner<Cube>
    {
        private CubeSpawnerConfig _config;

        private float _interval;
        private float _radius;

        private int _limit;
        private bool _hasLimit;

        [Inject]
        private void Construct(IStaticDataProvider staticDataProvider)
        {
            _config = staticDataProvider.GetConfig<CubeSpawnerConfig>();
            Initialize(_config.Interval, _config.Radius, _config.Limit, _config.HasLimit);
        }

        public event Action<Cube> CubeSpawned;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            YieldInstruction delay = new WaitForSeconds(_interval);

            while (CanSpawn())
            {
                Cube instance = Pool.Get(ComputeSpawnPosition(), Quaternion.identity, null);

                CubeSpawned?.Invoke(instance);

                yield return delay;
            }

            yield break;
        }

        private bool IsSpawnLimitReached()
        {
            return InstantiatedObjectsCount == _limit;
        }

        private bool CanSpawn()
        {
            return !_hasLimit || !IsSpawnLimitReached();
        }

        protected virtual Vector3 ComputeSpawnPosition()
        {
            Vector2 area = UnityEngine.Random.insideUnitCircle * _radius;
            Vector3 offset = new Vector3(area.x, 0, area.y);

            return transform.position + offset;
        }

        private void Initialize(float interval, float radius, int limit, bool hasLimit)
        {
            _interval = interval;
            _radius = radius;
            _limit = limit;
            _hasLimit = hasLimit;
        }
    }
}
