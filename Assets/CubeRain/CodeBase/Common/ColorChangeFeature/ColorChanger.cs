using Assets.CubeRain.CodeBase.Infrastructure.Utils.Randomization;
using System.Collections;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Common.ColorChangeFeature
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ColorChanger : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private Color _default;

        public bool IsInitialized { get; private set; } 

        public void Init(MeshRenderer meshRenderer) 
        {
            if (IsInitialized == false) 
            { 
                _meshRenderer = meshRenderer;
                IsInitialized = true;
            }
        }

        private void Start()
        {
            _default = _meshRenderer.material.color;
        }

        public void SetRandomColor()
        {
            _meshRenderer.material.color = new Color(Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat());
        }

        public void MakeTransparent(float time)
        {
            float transparencyAlpha = 0;

            StartCoroutine(ChangeColorAlpha(time, transparencyAlpha));
        }

        public void ResetColor()
        {
            _meshRenderer.material.color = _default;
        }

        private IEnumerator ChangeColorAlpha(float time, float targetAlpha)
        {
            float timer = 0;

            Color current = _meshRenderer.material.color;

            float delta = (current.a - targetAlpha) / time;

            while (timer < time)
            {
                current = _meshRenderer.material.color;

                _meshRenderer.material.color = 
                    new Color(current.r, current.g, current.b, Mathf.MoveTowards(current.a, targetAlpha, delta * Time.deltaTime));

                timer += Time.deltaTime;

                yield return null;
            }

            yield break;
        }
    }
}
