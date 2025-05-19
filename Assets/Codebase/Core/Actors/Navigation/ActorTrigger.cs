using System;
using UnityEngine;

namespace Codebase.Core.Actors
{
    [RequireComponent(typeof(Collider))]
    public class ActorTrigger<TActor> : MonoBehaviour where TActor : Actor
    {
        public event Action<TActor> OnEnter;
        public event Action<TActor> OnExit;
        public event Action<TActor, Vector3> OnEnterWithContactPoint;
        public event Action<TActor, Vector3> OnExitWithContactPoint;
        public Collider Collider => _collider;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ActorHitBox<TActor> hitbox))
            {
                OnEnter?.Invoke(hitbox.Owner);
                OnEnterWithContactPoint?.Invoke(hitbox.Owner, Collider.ClosestPoint(other.attachedRigidbody.transform.position));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ActorHitBox<TActor> hitbox))
            {
                OnExit?.Invoke(hitbox.Owner);
                OnExitWithContactPoint?.Invoke(hitbox.Owner, Collider.ClosestPoint(other.attachedRigidbody.transform.position));
            }
        }
    }
}