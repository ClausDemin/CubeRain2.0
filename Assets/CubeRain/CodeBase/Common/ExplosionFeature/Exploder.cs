using System.Collections.Generic;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Common.ExplosionFeature
{
    public class Exploder: MonoBehaviour
    {
        private float _explosionForce;
        private float _explosionRadius;

        public bool IsInitialized;

        public void Init(float explosionForce, float explosionRadius) 
        {
            if (IsInitialized == false) 
            { 
                _explosionForce = explosionForce;
                _explosionRadius = explosionRadius;
                
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
            Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, _explosionRadius);

            List<Rigidbody> involvedBodies = new List<Rigidbody>();

            foreach (Collider collider in objectsInRadius) 
            {
                if (collider.TryGetComponent(out Rigidbody rigidbody)) 
                { 
                    involvedBodies.Add(rigidbody);
                }
            }

            return involvedBodies;
        }
    }
}
