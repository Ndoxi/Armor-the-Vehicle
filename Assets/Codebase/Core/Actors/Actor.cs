using UnityEngine;

namespace Codebase.Core.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public abstract ActorHealth Health { get; }
        protected abstract ActorStateMachineBase StateMachine { get; }
        public abstract void OnDeath();

        private void FixedUpdate()
        {
            StateMachine.Update(Time.fixedDeltaTime);
        }
    }
}