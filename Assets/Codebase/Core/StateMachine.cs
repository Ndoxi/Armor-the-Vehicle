using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core
{
    public abstract class StateMachine<TState> where TState : class, IState
    {
        protected abstract Dictionary<Type, TState> States { get; }
        protected abstract string LogTag { get; }

        protected IState _currentState;
        protected ILogger _logger;

        public StateMachine(ILogger logger)
        {
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

        private IState GetState<T>() where T : class, IState
        {
            return States[typeof(T)];
        }
    }
}