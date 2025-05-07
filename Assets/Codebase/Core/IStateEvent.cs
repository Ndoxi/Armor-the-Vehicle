using System;

namespace Codebase.Core
{
    public interface IStateEvent<TState> where TState : class, IState
    {
        void AddListener<T>(Action callback) where T : class, TState;
        void RemoveListener<T>(Action callback) where T : class, TState;
    }
}