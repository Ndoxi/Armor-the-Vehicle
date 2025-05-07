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

        protected T GetFromPoolOrSpawn()
        {
            if (!_pool.Empty())
                return _pool.GetItem();
            return Spawn();
        }

        protected T Spawn()
        {
            return _factory.Create<T>();
        }
    }
}