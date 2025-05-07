using Zenject;

namespace Codebase.Core.Actors
{
    public class BulletSpawner : Spawner<BulletActor>
    {
        protected override int InitialPoolSize => 33;

        public BulletSpawner(ActorsFactory factory, IInstantiator instantiator) : base(factory, instantiator) { }
    }
}