using Zenject;

namespace Codebase.Core.Actors
{
    public class ActorStateFactory
    {
        private readonly IInstantiator _instantiator;

        public ActorStateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IActorState Create<T>() where T : IActorState
        {
            return _instantiator.Instantiate<T>();
        }
    }
}