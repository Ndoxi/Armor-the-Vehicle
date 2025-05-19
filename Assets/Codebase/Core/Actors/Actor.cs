using System;
using UnityEngine;

namespace Codebase.Core.Actors
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Actor : MonoBehaviour
    {
        public event Action OnDeathEvent;
        
        public abstract ActorHealth Health { get; }
        protected abstract ActorStateMachineBase StateMachine { get; }
        protected abstract Rigidbody Rigidbody { get; }

        private void FixedUpdate()
        {
            StateMachine.Update(Time.fixedDeltaTime);
        }

        public void SetPosition(Vector3 position)
        {
            Rigidbody.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            Rigidbody.rotation = rotation;
        }

        public Vector3 GetPosition()
        {
            return Rigidbody.position;
        }

        public Quaternion GetRotation()
        {
            return Rigidbody.rotation;
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            Rigidbody.position = position;
            Rigidbody.rotation = rotation;
        }

        public virtual void OnDeath()
        {
            OnDeathEvent?.Invoke();
        }

        public abstract void HardReset();
    }
}