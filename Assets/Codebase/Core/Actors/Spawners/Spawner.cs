using System;
using System.Collections.Generic;
using Zenject;

namespace Codebase.Core.Actors
{
    public abstract class Spawner<T> where T : Actor
    {
        protected abstract int InitialPoolSize { get; }

        protected readonly ActorsFactory _factory;
        protected readonly Pool<T> _pool;
        private readonly Dictionary<T, Action> _despawnHandlers; 

        public Spawner(ActorsFactory factory,
                       IInstantiator instantiator)
        {
            _factory = factory;
            _pool = new Pool<T>(instantiator);
            _despawnHandlers = new Dictionary<T, Action>();
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

        protected void Despawn(T actor)
        {
            if (_despawnHandlers.TryGetValue(actor, out Action handler))
            {
                actor.OnDeathEvent -= handler;
                _despawnHandlers.Remove(actor);
            }

            actor.HardReset();
            _pool.StoreItem(actor);
        }

        protected T GetFromPoolOrSpawn()
        {
            if (!_pool.IsEmpty())
                return GetFromPool();
            return Spawn();
        }

        protected T Spawn()
        {
            var actor =  _factory.Create<T>();

            _despawnHandlers[actor] = DespawnHandler;
            actor.OnDeathEvent += DespawnHandler;

            return actor;

            void DespawnHandler() => Despawn(actor);
        }

        protected T GetFromPool()
        {
            var actor = _pool.GetItem();

            if (_despawnHandlers.TryGetValue(actor, out var existing))
                actor.OnDeathEvent -= existing;
            _despawnHandlers[actor] = DespawnHandler;
            actor.OnDeathEvent += DespawnHandler;

            return actor;

            void DespawnHandler() => Despawn(actor);
        }
    }
}