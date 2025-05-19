using Zenject;

namespace Codebase.Core.Actors
{
    public class BulletActorVFXController : ActorVFXController
    {
        protected override int InitialPoolSize => 10;
        protected override string ResourcePath => "Effects/BulletHitParticles";

        public BulletActorVFXController(IInstantiator instantiator, IResourceLoader resourceLoader) : base(instantiator, resourceLoader) { }
    }
}