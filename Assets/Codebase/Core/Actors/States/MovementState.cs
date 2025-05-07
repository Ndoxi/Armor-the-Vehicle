using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Codebase.Core.Actors
{
    public class MovementState : IActorState
    {
        private readonly ActorMovement _actorMovement;
        private readonly IPathBuilder _pathBuilder;

        public MovementState(ActorMovement actorMovement, IPathBuilder pathBuilder)
        {
            _actorMovement = actorMovement;
            _pathBuilder = pathBuilder;
        }

        public void Enter() { }

        public void Exit() { }

        public void Update(float deltaTime)
        {
            _actorMovement.MoveTowards(_pathBuilder.GetDirection(), deltaTime);
        }
    }
}