using Assets.CubeRain.CodeBase.Service.CollisionDetection.Interface;
using System;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Service.CollisionDetection
{
    public class CollisionChecker : MonoBehaviour, ICollisionChecker
    {
        public event Action<Collision> CollisionEntered;
        public event Action<Collision> CollisionStay;

        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEntered?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            CollisionStay?.Invoke(collision);
        }

        private void OnTriggerEnter(Collider collision)
        {
            TriggerEntered?.Invoke(collision);
        }

        private void OnTriggerExit(Collider collision)
        {
            TriggerExited?.Invoke(collision);
        }
    }
}
