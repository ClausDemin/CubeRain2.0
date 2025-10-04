using Assets.CubeRain.CodeBase.Common.ColorChangeFeature;
using Assets.CubeRain.CodeBase.Infrastructure.ObjectPool.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.Utils.Randomization;
using Assets.CubeRain.CodeBase.Service.CollisionDetection;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Common.CubeFeature
{
    [RequireComponent(typeof(MeshRenderer), typeof(ColorChanger), typeof(CollisionChecker))]
    public class Cube : MonoBehaviour, IPooledInstance
    {
        private CollisionChecker _collisionChecker;
        private ColorChanger _colorChanger;

        private float _minLifeTime;
        private float _maxLifeTime;

        private bool _isGroundTouched;

        public void Construct(float minLifeTime, float maxLifeTime) 
        { 
            _minLifeTime = minLifeTime;
            _maxLifeTime = maxLifeTime;
        }

        public event Action<IPooledInstance> Released;
        public event Action<IPooledInstance> Disposed;

        private void Awake()
        {
            _collisionChecker = GetComponent<CollisionChecker>();
            _colorChanger = GetComponent<ColorChanger>();
        }

        private void OnDestroy()
        {
            Disposed?.Invoke(this);
        }

        private void OnEnable()
        {
            _collisionChecker.CollisionEntered += OnGroundTouched;
        }

        private void OnDisable()
        {
            _collisionChecker.CollisionEntered -= OnGroundTouched;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Reset()
        {
            _colorChanger.ResetColor();
            _isGroundTouched = false;
        }

        private void OnGroundTouched(Collision collision) 
        {
            if (!_isGroundTouched && collision.gameObject.TryGetComponent(out Ground ground)) 
            {
                _isGroundTouched = true;
                _colorChanger.SetRandomColor();

                StartCoroutine(DisableAfterTime(SelectLifetime()));
            }
        }

        private float SelectLifetime() 
        {
            return Randomizer.GetRandomFloat(_minLifeTime, _maxLifeTime);
        }

        private IEnumerator DisableAfterTime(float lifeTime) 
        {
            YieldInstruction delay = new WaitForSeconds(lifeTime);

            yield return delay;

            Released?.Invoke(this);

            yield break;
        }
    }
}
