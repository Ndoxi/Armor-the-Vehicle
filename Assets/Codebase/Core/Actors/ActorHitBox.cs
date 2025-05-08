using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class ActorHitBox<T> : MonoBehaviour where T : Actor
    {
        public T Owner => _owner;
        private T _owner;

        [Inject]
        private void Construct(T owner)
        {
            _owner = owner;
        }
    }
}