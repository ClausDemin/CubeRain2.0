using System;
using UnityEngine;

namespace Assets.CubeRain.CodeBase.Service.CollisionDetection.Interface
{
    public interface ICollisionChecker
    {
        public event Action<Collision> CollisionEntered;
        public event Action<Collision> CollisionStay;

        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;
    }
}
