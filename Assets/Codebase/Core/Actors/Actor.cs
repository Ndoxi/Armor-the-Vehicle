using UnityEngine;

namespace Codebase.Core.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public abstract ActorHealth Health { get; }
        protected abstract ActorStateMachineBase StateMachine { get; }

        private void FixedUpdate()
        {
            StateMachine.Update(Time.fixedDeltaTime);
        }

        public abstract void HardReset();
        public abstract void OnDeath();
    }
}