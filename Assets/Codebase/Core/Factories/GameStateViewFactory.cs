using Codebase.Core.Views;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Factories
{
    public class GameStateViewFactory
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly IInstantiator _instantiator;
        private readonly GameObject _root;
        private readonly Dictionary<Type, string> _resourcesMap;

        public GameStateViewFactory(IResourceLoader resourceLoader, IInstantiator instantiator)
        {
            _resourceLoader = resourceLoader;
            _instantiator = instantiator;
            _root = CreateRoot();
            _resourcesMap = new Dictionary<Type, string>()
            {
                { typeof(PreparationView), "Views/PreparationStateView" },
                { typeof(GameplayLoopStateView), "Views/GameplayLoopStateView" }
            };
        }

        public IStateView Create<T>() where T : UnityEngine.Object, IStateView
        {
            var key = GetResourceKey<T>();
            var prefab = _resourceLoader.Load<T>(key);
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, _root.transform);
        }

        private string GetResourceKey<T>()
        {
            return _resourcesMap[typeof(T)];
        }

        private GameObject CreateRoot()
        {
            return _instantiator.CreateEmptyGameObject("[StateViews]");
        }
    }
}