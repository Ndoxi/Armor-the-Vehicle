using Codebase.Core.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core
{
    public class GameStateMachine : StateMachine<IState>
    {
        protected override string LogTag => "GameStateMachine";
        protected override Dictionary<Type, IState> States => _states;
        private readonly Dictionary<Type, IState> _states;
        
        public GameStateMachine(GameStateMachineFactory gameStateMachineFactory, ILogger logger) : base(logger)
        {
            _states = new Dictionary<Type, IState>()
            {
                { typeof(InitializationState), gameStateMachineFactory.Create<InitializationState>() },
                { typeof(PreparationState), gameStateMachineFactory.Create<PreparationState>() },
                { typeof(GameplayLoopState), gameStateMachineFactory.Create<GameplayLoopState>() }
            };
        }
    }
}