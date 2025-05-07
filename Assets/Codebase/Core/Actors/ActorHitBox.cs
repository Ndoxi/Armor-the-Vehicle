using UnityEngine;

namespace Codebase.Core.Actors
{
    public class ActorHitBox<T> : MonoBehaviour where T : Actor
    {
        public T Owner => _owner;
        [SerializeField] private T _owner;
    }
}