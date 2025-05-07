using Codebase.Core.LevelBuilders;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class EnemiesSpawner
    {
        private const int InitialPoolSize = 33;
        private const int MinEnemiesPerChunk = 7;
        private const int MaxEnemiesPerChunk = 13;

        private readonly ActorsFactory _factory;
        private readonly IRandom _random;
        private readonly Pool<StickmanActor> _pool;
        private readonly Dictionary<Chunk, List<StickmanActor>> _managedEnemiesMap;

        public EnemiesSpawner(ActorsFactory factory,
                              IRandom random, 
                              IInstantiator instantiator)
        {
            _factory = factory;
            _random = random;
            _pool = new Pool<StickmanActor>(instantiator);
            _managedEnemiesMap = new Dictionary<Chunk, List<StickmanActor>>();
        }

        public void Initialize()
        {
            InitializePool();
        }

        public void PopulateChunk(Chunk chunk)
        {
            int toSpawn = _random.Range(MinEnemiesPerChunk, MaxEnemiesPerChunk);
            var enemies = new List<StickmanActor>();

            for (int i = 0; i < toSpawn; i++)
            {
                var enemy = GetFromPoolOrSpawn();

                var spawnPosition = chunk.transform.TransformPoint(GetRandomLocalPosition(chunk.LocalSpawnArea));
                enemy.transform.SetPositionAndRotation(spawnPosition,
                                                       GetRandomRotation());;

                enemies.Add(enemy);
            }

            _managedEnemiesMap.Add(chunk, enemies);


            Vector3 GetRandomLocalPosition(Bounds bounds)
            {
                return new Vector3(_random.Range(bounds.min.x, bounds.max.x),
                                   _random.Range(bounds.min.y, bounds.max.y),
                                   _random.Range(bounds.min.z, bounds.max.z));
            }

            Quaternion GetRandomRotation()
            {
                return Quaternion.Euler(0f, _random.Range(0f, 360f), 0f);
            }
        }

        public void ClearChunk(Chunk chunk)
        {
            if (!_managedEnemiesMap.ContainsKey(chunk))
                return;

            var enemiesToClear = _managedEnemiesMap[chunk];
            foreach (var enemy in enemiesToClear)
            {
                enemy.HardReset();
                _pool.StoreItem(enemy);
            }

            _managedEnemiesMap.Remove(chunk);
        }

        private void InitializePool()
        {
            var initialEnemies = new List<StickmanActor>(InitialPoolSize);
            for (int i = 0; i < InitialPoolSize; i++)
                initialEnemies.Add(SpawnEnemy());

            _pool.Initialize(initialEnemies);
        }

        private StickmanActor GetFromPoolOrSpawn()
        {
            if (!_pool.Empty())
                return _pool.GetItem();
            return SpawnEnemy();
        }

        private StickmanActor SpawnEnemy()
        {
            return _factory.Create<StickmanActor>();
        }
    }
}