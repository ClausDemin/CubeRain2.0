using System.Collections.Generic;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Common.ExplosionFeature
{
    public class Exploder: MonoBehaviour
    {
        private float _explosionForce;
        private float _explosionRadius;
        private int _maximumTargets;

        private Collider[] _objectsInRadius;

        public bool IsInitialized { get; private set; }

        public void Init(float explosionForce, float explosionRadius, int maximumTargets) 
        {
            if (IsInitialized == false) 
            { 
                _explosionForce = explosionForce;
                _explosionRadius = explosionRadius;
                _maximumTargets = maximumTargets;

                _objectsInRadius = new Collider[_maximumTargets];

                IsInitialized = true;
            }
        }

        public void Explode() 
        {
            foreach (Rigidbody target in GetTargets()) 
            {
                target.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        private List<Rigidbody> GetTargets() 
        {
            Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _objectsInRadius);

            List<Rigidbody> involvedBodies = new List<Rigidbody>();

            foreach (Collider collider in _objectsInRadius) 
            {
                if (collider != null && collider.TryGetComponent(out Rigidbody rigidbody)) 
                { 
                    involvedBodies.Add(rigidbody);
                }
            }

            return involvedBodies;
        }
    }
}
