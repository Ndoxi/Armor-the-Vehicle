using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class ActorsFactory
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly IInstantiator _instantiator;
        private readonly Dictionary<Type, string> _resourcesMap;

        public ActorsFactory(IResourceLoader resourceLoader, IInstantiator instantiator)
        {
            _resourceLoader = resourceLoader;
            _instantiator = instantiator;

            _resourcesMap = new Dictionary<Type, string>() 
            {
                { typeof(PlayerActor), "Actors/PlayerActor" },
                { typeof(StickmanActor), "Actors/StickmanActor" },
                { typeof(BulletActor), "Actors/BulletActor" }
            };
        }

        public T Create<T>() where T : Actor
        {
            var key = GetResourceKey<T>();
            var prefab = _resourceLoader.Load<T>(key);
            return _instantiator.InstantiatePrefabForComponent<T>(prefab);
        }

        private string GetResourceKey<T>()
        {
            return _resourcesMap[typeof(T)];
        }
    }
}