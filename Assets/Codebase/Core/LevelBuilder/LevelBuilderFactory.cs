using UnityEngine;
using Zenject;

namespace Codebase.Core.LevelBuilders
{
    public class LevelBuilderFactory
    {
        private const string ResourceKey = "LevelBuilder/RoadChunk";
        private readonly Chunk _chunkPrefab;
        private readonly IInstantiator _instantiator;

        public LevelBuilderFactory(IResourceLoader resourceLoader, IInstantiator instantiator)
        {
            _chunkPrefab = resourceLoader.Load<Chunk>(ResourceKey);
            _instantiator = instantiator;
        }

        public Chunk Create()
        {
            return _instantiator.InstantiatePrefabForComponent<Chunk>(_chunkPrefab);
        }

        public Chunk Create(Transform parent)
        {
            return _instantiator.InstantiatePrefabForComponent<Chunk>(_chunkPrefab, parent);
        }
    }
}