using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public class StickmanStateMachine : ActorStateMachineBase
    {
        protected override string LogTag => "StickmanStateMachine";
        protected override Dictionary<Type, IActorState> States => _states;
        private readonly Dictionary<Type, IActorState> _states;

        public StickmanStateMachine(ActorStateFactory factory, ILogger logger) : base(logger) 
        {
            _states = new Dictionary<Type, IActorState>()
            {
                { typeof(IdleState), factory.Create<IdleState>() },
                { typeof(MovementState), factory.Create<MovementState>() },
                { typeof(DeathState), factory.Create<DeathState>() }
            };
        }
    }
}