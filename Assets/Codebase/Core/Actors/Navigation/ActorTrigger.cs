using System;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public class ActorTrigger<TActor> : MonoBehaviour where TActor : Actor
    {
        public event Action<TActor> OnEnter;
        public event Action<TActor> OnExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ActorHitBox<TActor> hitbox))
                OnEnter?.Invoke(hitbox.Owner);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ActorHitBox<TActor> hitbox))
                OnExit?.Invoke(hitbox.Owner);
        }
    }
}