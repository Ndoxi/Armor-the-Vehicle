using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class ActorMovement : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        protected Rigidbody _rigidbody;

        [Inject]
        private void Construct(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public virtual void MoveTowards(Vector3 direction, float deltaTime)
        {
            _rigidbody.MovePosition(_rigidbody.position + _speed * deltaTime * direction);
        }
    }
}