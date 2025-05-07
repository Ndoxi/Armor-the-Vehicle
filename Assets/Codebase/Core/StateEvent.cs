using System;
using System.Collections.Generic;

namespace Codebase.Core
{
    public class StateEvent<TState> : IStateEvent<TState> where TState : class, IState
    {
        private readonly Dictionary<Type, List<Action>> _callbackMap;

        public StateEvent()
        {
            _callbackMap = new Dictionary<Type, List<Action>>();
        }

        public void AddListener<T>(Action callback) where T : class, TState
        {
            if (callback == null)
                throw new ArgumentException();

            var key = typeof(T);
            if (_callbackMap.ContainsKey(key))
                _callbackMap[key].Add(callback);
            else
                _callbackMap[key] = new List<Action>() { callback };
        }

        public void RemoveListener<T>(Action callback) where T : class, TState
        {
            if (callback == null)
                throw new ArgumentException();

            var key = typeof(T);
            if (_callbackMap.ContainsKey(key))
                _callbackMap[key].Remove(callback);
        }

        public void Invoke<T>() where T : class, TState
        {
            Invoke(typeof(T));
        }

        public void Invoke(Type type)
        {
            if (_callbackMap.ContainsKey(type))
                _callbackMap[type].ForEach(action => action.Invoke());
        }
    }
}