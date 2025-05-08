using System;
using System.Collections.Generic;

namespace Codebase.Core
{
    public class StateEvent<TState> : IStateEvent<TState> where TState : class, IState
    {
        private readonly Dictionary<Type, Action> _callbackMap;

        public StateEvent()
        {
            _callbackMap = new Dictionary<Type, Action>();
        }

        public void AddListener<T>(Action callback) where T : class, TState
        {
            var key = typeof(T);
            if (!_callbackMap.ContainsKey(key))
                _callbackMap[key] = null;
            _callbackMap[key] += callback ?? throw new ArgumentException(nameof(callback));
        }

        public void RemoveListener<T>(Action callback) where T : class, TState
        {
            var key = typeof(T);
            if (_callbackMap.ContainsKey(key))
                _callbackMap[key] -= callback ?? throw new ArgumentException(nameof(callback));
        }

        public void Invoke<T>() where T : class, TState
        {
            Invoke(typeof(T));
        }

        public void Invoke(Type type)
        {
            if (_callbackMap.ContainsKey(type))
                _callbackMap[type]?.Invoke();
        }
    }
}