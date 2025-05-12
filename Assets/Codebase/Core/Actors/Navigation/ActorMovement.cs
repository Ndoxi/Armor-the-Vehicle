using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    [RequireComponent(typeof(Rigidbody))]
    public class ActorMovement : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        protected Rigidbody _rigidbody;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public virtual void MoveTowards(Vector3 direction, float deltaTime)
        {
            _rigidbody.MovePosition(_rigidbody.position + _speed * deltaTime * direction);
        }
    }
}