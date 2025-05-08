using Codebase.Core.LevelBuilders;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class EnemiesSpawner : Spawner<StickmanActor>
    {
        protected override int InitialPoolSize => 33;
        private const int MinEnemiesPerChunk = 4;
        private const int MaxEnemiesPerChunk = 8;

        private readonly IRandom _random;
        private readonly Dictionary<Chunk, List<StickmanActor>> _managedEnemiesMap;

        public EnemiesSpawner(ActorsFactory factory,
                              IInstantiator instantiator,
                              IRandom random) : base(factory, instantiator)
        {
            _random = random;
            _managedEnemiesMap = new Dictionary<Chunk, List<StickmanActor>>();
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
    }
}