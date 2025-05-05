using Codebase.Core.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core
{
    public class GameStateMachine
    {
        private const string LogTag = "StateMachine";
        private readonly Dictionary<Type, IState> _states;
        private readonly ILogger _logger;
        private IState _currentState;
        
        public GameStateMachine(GameStateMachineFactory gameStateMachineFactory, ILogger logger)
        {
            _states = new Dictionary<Type, IState>()
            {
                { typeof(PreparationState), gameStateMachineFactory.Create<PreparationState>() },
                { typeof(GameplayLoopState), gameStateMachineFactory.Create<GameplayLoopState>() }
            };

            _logger = logger;
        }

        public void EnterState<T>() where T : class, IState
        {
            var newState = GetState<T>();
            if (_currentState != null && newState.GetType() == _currentState.GetType())
            {
                _logger.LogWarning(LogTag, $"State machine already in {typeof(T)} state!");
                return;
            }

            ExitCurrentState();
            _currentState = newState;
            newState.Enter();
        }

        private void ExitCurrentState()
        {
            _currentState?.Exit();
        }

        private IState GetState<T>() where  T : class, IState 
        {
            return _states[typeof(T)];
        }
    }
}