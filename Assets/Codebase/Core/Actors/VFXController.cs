using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public abstract class VFXController<T> : IDisposable where T : MonoBehaviour, IVisualEffect
    {
        protected abstract int InitialPoolSize { get; }
        protected abstract string ResourcePath { get; }

        private readonly IInstantiator _instantiator;
        private readonly Pool<T> _pool;
        private readonly T _vfxTemplate;
        private readonly CancellationTokenSource _disposeCancellationTokenSource;

        public VFXController(IInstantiator instantiator, IResourceLoader resourceLoader)
        {
            _instantiator = instantiator;
            _pool = new Pool<T>(instantiator);
            _vfxTemplate = resourceLoader.Load<T>(ResourcePath);
            _disposeCancellationTokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            InitializePool();
        }

        public void Play(Vector3 position, Quaternion rotation)
        {
            PlayAsync(position, rotation).Forget();
        }

        private void InitializePool()
        {
            var initialLoad = new List<T>();
            for (int i = 0; i < InitialPoolSize; i++)
                initialLoad.Add(Spawn());

            _pool.Initialize(initialLoad);
        }

        private async UniTaskVoid PlayAsync(Vector3 position, Quaternion rotation)
        {
            var vfx = GetOrSpawn();
            vfx.transform.SetPositionAndRotation(position, rotation);

            await vfx.Play(_disposeCancellationTokenSource.Token);
            _pool.StoreItem(vfx);
        }

        private T Spawn()
        {
            return _instantiator.InstantiatePrefabForComponent<T>(_vfxTemplate);
        }

        private T GetOrSpawn()
        {
            if (_pool.IsEmpty())
                return Spawn();
            return _pool.GetItem();
        }

        public void Dispose()
        {
            _disposeCancellationTokenSource.Cancel();
            _disposeCancellationTokenSource.Dispose();
        }
    }
}