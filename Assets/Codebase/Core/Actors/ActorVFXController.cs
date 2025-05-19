using Zenject;

namespace Codebase.Core.Actors
{
    public abstract class ActorVFXController : VFXController<ActorParticleVFX>
    {
        public ActorVFXController(IInstantiator instantiator, IResourceLoader resourceLoader) : base(instantiator, resourceLoader) { }
    }
}