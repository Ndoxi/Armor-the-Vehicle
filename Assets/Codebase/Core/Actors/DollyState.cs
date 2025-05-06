using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Codebase.Core.Actors
{
    public class DollyState : IActorState
    {
        private readonly Actor _actor;
        private readonly ActorMovement _actorMovement;

        public DollyState(Actor actor)
        {
            _actor = actor;
            _actorMovement = actor.GetComponent<ActorMovement>();
        }

        public void Enter()
        {
            _actorMovement.MovementEnabled = true;
        }

        public void Exit()
        {
            _actorMovement.MovementEnabled = false;
        }
    }
}