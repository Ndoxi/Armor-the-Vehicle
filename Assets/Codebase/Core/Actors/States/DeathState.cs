using Zenject;

namespace Codebase.Core.Actors
{
    public class DeathState : IActorState
    {
        protected readonly Actor _parent;

        public DeathState(Actor parent)
        {
            _parent = parent;
        }

        public virtual void Enter() 
        {
            _parent.OnDeath();
        }

        public virtual void Exit() { }
        public virtual void Update(float deltaTime) { }
    }
}