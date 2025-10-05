using Assets.CubeRain.CodeBase.Common.ColorChangeFeature;
using Assets.CubeRain.CodeBase.Common.ExplosionFeature;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.Utils.Randomization;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Common.BombFeature
{
    [RequireComponent(typeof(MeshRenderer), typeof(ColorChanger), typeof(Exploder))]
    public class Bomb : MonoBehaviour, IPooledInstance
    {
        private ColorChanger _colorChanger;
        private Exploder _exploder;

        private float _minLifeTime;
        private float _maxLifeTime;

        public void Construct(float minLifeTime, float maxLifeTime) 
        {
            if (IsInitialized == false) 
            { 
                _minLifeTime = minLifeTime;
                _maxLifeTime = maxLifeTime;
                
                IsInitialized = true;
            }
        }

        public event Action<IPooledInstance> Released;
        public event Action<IPooledInstance> Disposed;

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            _colorChanger = GetComponent<ColorChanger>();
            _exploder = GetComponent<Exploder>();
        }

        private void OnDestroy()
        {
            Disposed?.Invoke(this);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);

            float lifeTime = SelectLifeTime();

            _colorChanger.MakeTransparent(lifeTime);
            StartCoroutine(ExplodeAfterTime(lifeTime));
        }

        public void Reset()
        {
            _colorChanger.ResetColor();
        }

        private IEnumerator ExplodeAfterTime(float time) 
        { 
            YieldInstruction delay = new WaitForSeconds(time);

            yield return delay;

            _exploder.Explode();

            Released?.Invoke(this);

            yield break;
        }

        private float SelectLifeTime() 
        { 
            return Randomizer.GetRandomFloat(_minLifeTime, _maxLifeTime);
        }
    }
}
