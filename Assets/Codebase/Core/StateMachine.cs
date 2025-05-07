using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core
{
    public abstract class StateMachine<TState> where TState : class, IState
    {
        public IStateEvent<TState> OnEnterEvent => _onEnter;
        public IStateEvent<TState> OnExitEvent => _onExit;

        protected abstract Dictionary<Type, TState> States { get; }
        protected abstract string LogTag { get; }

        protected TState _currentState;
        protected ILogger _logger;
        private readonly StateEvent<TState> _onEnter;
        private readonly StateEvent<TState> _onExit;

        public StateMachine(ILogger logger)
        {
            _logger = logger;
            _onEnter = new StateEvent<TState>();
            _onExit = new StateEvent<TState>();
        }

        public void EnterState<T>() where T : class, TState
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
            _onEnter.Invoke<T>();
        }

        private void ExitCurrentState()
        {
            if (_currentState == null)
                return;

            _currentState.Exit();
            _onExit.Invoke(_currentState.GetType());
        }

        private TState GetState<T>() where T : class, TState
        {
            return States[typeof(T)];
        }
    }
}