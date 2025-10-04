using Assets.CubeRain.CodeBase.Common.BombFeature;
using Assets.CubeRain.CodeBase.Infrastructure.Configs.Attributes;
using Assets.CubeRain.CodeBase.Infrastructure.Configs.Interface;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.Configs
{
    [ResourceOnly]
    [CreateAssetMenu(menuName = "Configs/Bomb", fileName = "BombConfig")]
    public class BombConfig : ScriptableObject, IConfig
    {
        [SerializeField][Range(0, 2000)] private float _explosionForce;
        [SerializeField][Range(0, 100)] private float _explosionRadius;

        [field: SerializeField] public Bomb Prefab { get; private set; }
        [field: SerializeField] public float MinLifetime { get; private set; }
        [field: SerializeField] public float MaxLifetime { get; private set; }
        
        public float ExplosionForce => _explosionForce;
        public float ExplosionRadius => _explosionRadius;
    }
}
