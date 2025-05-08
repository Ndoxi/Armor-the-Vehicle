using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public class BulletActorStateMachine : ActorStateMachineBase
    {
        protected override string LogTag => "BulletActorStateMachine";
        protected override Dictionary<Type, IActorState> States => _states;
        private readonly Dictionary<Type, IActorState> _states;

        public BulletActorStateMachine(ActorStateFactory factory, ILogger logger) : base(logger)
        {
            _states = new Dictionary<Type, IActorState>()
            {
                { typeof(IdleState), factory.Create<IdleState>() },
                { typeof(BulletMovementState), factory.Create<BulletMovementState>() },
                { typeof(DeathState), factory.Create<DeathState>() }
            };
        }
    }
}