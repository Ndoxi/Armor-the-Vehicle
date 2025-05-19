using Codebase.Core.Actors;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Codebase.Core.LevelBuilders
{
    public class LevelBuilder
    {
        private const int InitialChunksCount = 6;
        private const int MinActiveLoopChunksCount = 4;
        private const int LoopUpdateIntervalMs = 250;
        private const string LogTag = "LevelBuilder";

        private readonly LevelBuilderFactory _factory;
        private readonly List<Chunk> _activeChunks;
        private readonly EnemiesSpawner _spawner;
        private readonly Transform _levelStartConnection;
        private readonly ILogger _logger;
        private readonly GameObject _root;
        private readonly Pool<Chunk> _pool;
        private CancellationTokenSource _loopCancellationTokenSource;

        public LevelBuilder(LevelBuilderFactory factory,
                            IInstantiator instantiator,
                            EnemiesSpawner spawner,
                            Transform levelStartConnection,
                            ILogger logger)
        {
            _activeChunks = new List<Chunk>(6);
            _factory = factory;
            _spawner = spawner;
            _levelStartConnection = levelStartConnection;
            _logger = logger;
            _root = CreateRoot(instantiator);
            _pool = new Pool<Chunk>(instantiator);
        }

        public void Initialize()
        {
            InitializePool();
        }

        public void ActivateChunksLoading(Actor actor)
        {
            _loopCancellationTokenSource?.Cancel();
            _loopCancellationTokenSource?.Dispose();
            _loopCancellationTokenSource = new CancellationTokenSource();

            RunChunksManagmentLoop(actor.transform, _loopCancellationTokenSource.Token).Forget();
        }

        public void DeactivateChunksLoading()
        {
            _loopCancellationTokenSource?.Cancel();
            _loopCancellationTokenSource?.Dispose();
            _loopCancellationTokenSource = null;
        }

        public void ClearChunks()
        {
            foreach (var activeChunk in _activeChunks)
                _spawner.ClearChunk(activeChunk);
        }

        public void DespawnChunks()
        {
            foreach (var activeChunk in _activeChunks)
                _pool.StoreItem(activeChunk);
            _activeChunks.Clear();
        }

        private async UniTaskVoid RunChunksManagmentLoop(Transform target, CancellationToken cancellationToken)
        {
            if (_activeChunks.Count < MinActiveLoopChunksCount)
            {
                int toSpawn = MinActiveLoopChunksCount - _activeChunks.Count;
                for (int i = 0; i < toSpawn; i++)
                    SpawnChunk();
            }

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Delay(LoopUpdateIntervalMs, cancellationToken: cancellationToken);

                    var targetChunk = _activeChunks[^2];
                    if (targetChunk.GetBounds().Contains(target.position))
                    {
                        DestroyChunk();
                        SpawnChunk();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.Log(LogTag, "Chunks managment loop cancelled");
            }
        }

        private void SpawnChunk()
        {
            var newChunk = GetFromPoolOrSpawn();
            newChunk.transform.SetParent(_root.transform);

            if (_activeChunks.Count > 0)
                ConnectChunk(newChunk, _activeChunks.Last());
            else
                ConnectChunk(newChunk, _levelStartConnection.position);

            _spawner.PopulateChunk(newChunk);
            _activeChunks.Add(newChunk);
        }

        private void DestroyChunk()
        {
            if (_activeChunks.Count == 0)
                return;

            var chunk = _activeChunks[0];
            _activeChunks.RemoveAt(0);
            _spawner.ClearChunk(chunk);
            _pool.StoreItem(chunk);
        }

        private void InitializePool()
        {
            var initialChunks = new List<Chunk>(InitialChunksCount);
            for (int i = 0; i < InitialChunksCount; i++)
                initialChunks.Add(_factory.Create());

            _pool.Initialize(initialChunks);
        }

        private void ConnectChunk(Chunk chunk, Vector3 target)
        {
            Vector3 offset = chunk.StartPoint - chunk.transform.position;
            chunk.transform.position = target - offset;

            chunk.RecalculateBounds();
        }        
        
        private void ConnectChunk(Chunk chunk, Chunk target)
        {
            ConnectChunk(chunk, target.EndPoint);
        }

        private Chunk GetFromPoolOrSpawn()
        {
            if (!_pool.IsEmpty())
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