using Assets.CubeRain.CodeBase.Common.CubeFeature;
using Assets.CubeRain.CodeBase.Infrastructure.Configs.Attributes;
using Assets.CubeRain.CodeBase.Infrastructure.Configs.Interface;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Infrastructure.Configs
{
    [ResourceOnly]
    [CreateAssetMenu(menuName = "Configs/Cube", fileName = "CubeConfig")]
    public class CubeConfig: ScriptableObject, IConfig
    {
        [field: SerializeField] public Cube Prefab { get; private set; }
        [field: SerializeField] public float MinLifetime { get; private set; }
        [field: SerializeField] public float MaxLifetime { get; private set; }
        [field: SerializeField] public Color32 DefaultColor { get; private set; }

        private void OnValidate()
        {
            if (MinLifetime > MaxLifetime) 
            { 
                MinLifetime = MaxLifetime;
            }
        }
    }
}
