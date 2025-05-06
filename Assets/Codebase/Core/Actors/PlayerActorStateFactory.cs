using Zenject;

namespace Codebase.Core.Actors
{
    public class PlayerActorStateFactory
    {
        private readonly IInstantiator _instantiator;

        public PlayerActorStateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IActorState Create<T>() where T : IActorState
        {
            return _instantiator.Instantiate<T>();
        }
    }
}