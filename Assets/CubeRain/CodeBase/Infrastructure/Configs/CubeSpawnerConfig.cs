using Assets.CubeRain.CodeBase.Infrastructure.Configs.Attributes;
using Assets.CubeRain.CodeBase.Infrastructure.Configs.Interface;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.Configs
{
    [ResourceOnly]
    [CreateAssetMenu(menuName = "Configs/Spawners/CubeSpawner", fileName = "CubeSpawnerConfig")]
    public class CubeSpawnerConfig: ScriptableObject, IConfig
    {
        [SerializeField][Range(0, 10)] private float _interval;
        [SerializeField][Min(1)] private float _radius;
        [SerializeField][Min(1)] private int _limit;
        [SerializeField] private bool _hasLimit;

        public float Interval => _interval;
        public float Radius => _radius;
        public int Limit => _limit;
        public bool HasLimit => _hasLimit;
    }
}
