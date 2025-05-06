using System;

namespace Codebase.Core.Actors
{
    public class IdleState : IActorState
    {
        private readonly ActorMovement _actorMovement;

        public IdleState(Actor actor)
        {
            _actorMovement = actor.GetComponent<ActorMovement>();
        }

        public void Enter()
        {
            _actorMovement.MovementEnabled = false;
        }

        public void Exit()
        {
        }
    }
}