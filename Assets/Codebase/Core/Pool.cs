using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Core
{
    public class Pool<TItem> where TItem : MonoBehaviour
    {
        private readonly List<TItem> _content;
        private readonly GameObject _root;

        public Pool(IInstantiator instantiator)
        {
            _content = new List<TItem>();
            _root = CreateRoot(instantiator);
        }

        public void Initialize(IEnumerable<TItem> items)
        {
            _content.AddRange(items);
            foreach (var item in items)
                item.transform.SetParent(_root.transform);
        }

        public void StoreItem(TItem item)
        {
            item.transform.SetParent(_root.transform);
            _content.Add(item);
        }

        public TItem GetItem()
        {
            var item = _content[0];
            item.transform.SetParent(null);
            _content.RemoveAt(0);
            return item;
        }

        public bool Empty()
        {
            return _content.Count == 0;
        }

        private GameObject CreateRoot(IInstantiator instantiator)
        {
            var root = instantiator.CreateEmptyGameObject("[Pool]");
            root.transform.position = Vector3.zero;
            root.SetActive(false);
            return root;
        }
    }
}