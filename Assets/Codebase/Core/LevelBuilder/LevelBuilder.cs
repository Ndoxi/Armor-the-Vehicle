using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Codebase.Core.LevelBuilders
{
    public class LevelBuilder
    {
        private const int InitialChunksCount = 6;

        private readonly LevelBuilderFactory _factory;
        private readonly List<Chunk> _activeChunks;
        private readonly Transform _levelStartConnection;
        private readonly GameObject _root;
        private readonly Pool<Chunk> _pool;

        public LevelBuilder(LevelBuilderFactory factory, IInstantiator instantiator, Transform levelStartConnection)
        {
            _factory = factory;
            _activeChunks = new List<Chunk>(6);
            _levelStartConnection = levelStartConnection;
            _root = CreateRoot(instantiator);
            _pool = new Pool<Chunk>(instantiator);
        }

        public void Initialize()
        {
            AddInitialLoad();
        }

        public void SpawnChunk()
        {
            var newChunk = GetFromPoolOrSpawn();
            newChunk.transform.SetParent(_root.transform);

            if (_activeChunks.Count > 0)
                ConnectChunks(newChunk, _activeChunks.Last());
            else
                ConnectChunks(newChunk, _levelStartConnection.position);

            _activeChunks.Add(newChunk);
        }

        public void DestroyChunk()
        {
            if (_activeChunks.Count == 0)
                return;

            var chunk = _activeChunks[0];
            _activeChunks.RemoveAt(0);
            _pool.StoreItem(chunk);
        }

        private void AddInitialLoad()
        {
            var initialChunks = new List<Chunk>(InitialChunksCount);
            for (int i = 0; i < InitialChunksCount; i++)
                initialChunks.Add(_factory.Create());

            _pool.Initialize(initialChunks);
        }

        private void ConnectChunks(Chunk chunk, Vector3 target)
        {
            Vector3 offset = chunk.StartPoint - chunk.transform.position;
            chunk.transform.position = target - offset;
        }        
        
        private void ConnectChunks(Chunk chunk, Chunk target)
        {
            ConnectChunks(chunk, target.EndPoint);
        }

        private Chunk GetFromPoolOrSpawn()
        {
            if (!_pool.Empty())
                return _pool.GetItem();
            return _factory.Create();
        }

        private GameObject CreateRoot(IInstantiator instantiator)
        {
            var root = instantiator.CreateEmptyGameObject("[Level]");
            root.transform.position = Vector3.zero;
            return root;
        }
    }
}