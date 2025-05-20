using Zenject;

namespace Codebase.Core.Actors
{
    public class StickmanActorVFXController : ActorVFXController
    {
        protected override int InitialPoolSize => 15;
        protected override string ResourcePath => "Effects/StickmanExplosionParticles";

        public StickmanActorVFXController(IInstantiator instantiator, IResourceLoader resourceLoader) : base(instantiator, resourceLoader) { }
    }
}