using System;
using System.Collections.Generic;
using Zenject;
using Codebase.Core.Views;

namespace Codebase.Core.Factories
{
    public class GameStateMachineFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly GameStateViewFactory _viewFactory;
        private readonly Dictionary<Type, Func<IState>> _buiders; 

        public GameStateMachineFactory(IInstantiator instantiator, GameStateViewFactory viewFactory)
        {
            _instantiator = instantiator;
            _viewFactory = viewFactory;

            _buiders = new Dictionary<Type, Func<IState>>()
            {
                { typeof(PreparationState), PreparationStateBuilder }
            };
        }

        public IState Create<T>() where T : class, IState
        {
            if (_buiders.TryGetValue(typeof(T), out Func<IState> builder))
                return builder.Invoke();
            return DefaultBuilder<T>();
        }

        private IState PreparationStateBuilder()
        {
            var stateView = _viewFactory.Create<PreparationView>();
            var additionalArgs = new object[] { stateView };
            return _instantiator.Instantiate<PreparationState>(additionalArgs);
        }

        private IState DefaultBuilder<T>() where T : class, IState
        {
            return _instantiator.Instantiate<T>();
        }
    }
}