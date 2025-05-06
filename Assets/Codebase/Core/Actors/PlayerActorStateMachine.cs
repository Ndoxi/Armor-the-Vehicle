using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public class PlayerActorStateMachine : ActorStateMachineBase
    {
        protected override string LogTag => "PlayerActorStateMachine";
        protected override Dictionary<Type, IActorState> States => _states;

        private readonly Dictionary<Type, IActorState> _states;

        public PlayerActorStateMachine(PlayerActorStateFactory factory, ILogger logger) : base(logger)
        {
            _states = new Dictionary<Type, IActorState>() 
            {
                { typeof(IdleState), factory.Create<IdleState>() },
                { typeof(DollyState), factory.Create<DollyState>() }
            };
        }
    }
}