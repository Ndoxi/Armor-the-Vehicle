using System.Collections.Generic;
using Zenject;

namespace Codebase.Core.Actors
{
    public abstract class Spawner<T> where T : Actor
    {
        protected abstract int InitialPoolSize { get; }

        protected readonly ActorsFactory _factory;
        protected readonly Pool<T> _pool;

        public Spawner(ActorsFactory factory,
                       IInstantiator instantiator)
        {
            _factory = factory;
            _pool = new Pool<T>(instantiator);
        }

        public void Initialize()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            var initialActors = new List<T>(InitialPoolSize);
            for (int i = 0; i < InitialPoolSize; i++)
                initialActors.Add(Spawn());

            _pool.Initialize(initialActors);
        }

        private void Despawn(T actor)
        {
            actor.HardReset();
            _pool.StoreItem(actor);
        }

        protected T GetFromPoolOrSpawn()
        {
            if (!_pool.Empty())
            {
                var actor = _pool.GetItem();
                actor.OnDeathEvent += () => Despawn(actor);
                return actor;
            }
            return Spawn();
        }

        protected T Spawn()
        {
            var actor =  _factory.Create<T>();
            actor.OnDeathEvent += () => Despawn(actor);
            return actor;
        }
    }
}