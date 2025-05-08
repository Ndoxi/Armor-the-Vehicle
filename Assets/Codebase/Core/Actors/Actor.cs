using System;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public event Action OnDeathEvent;
        
        public abstract ActorHealth Health { get; }
        protected abstract ActorStateMachineBase StateMachine { get; }

        private void FixedUpdate()
        {
            StateMachine.Update(Time.fixedDeltaTime);
        }        

        public virtual void OnDeath()
        {
            OnDeathEvent?.Invoke();
        }

        public virtual void HardReset() 
        {
            OnDeathEvent = null;
        }
    }
}