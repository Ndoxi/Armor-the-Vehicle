namespace Codebase.Core.Actors
{
    public class DeathState : IActorState
    {
        private readonly Actor _parent;

        public DeathState(Actor parent)
        {
            _parent = parent;
        }

        public void Enter() 
        {
            _parent.OnDeath();
        }

        public void Exit() { }
        public void Update(float deltaTime) { }
    }
}