using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class ActorMovement : MonoBehaviour
    {
        public bool MovementEnabled;

        [SerializeField] private float _speed;
        private IPathBuilder _pathBuilder;

        [Inject]
        private void Construct(IPathBuilder pathBuilder)
        {
            _pathBuilder = pathBuilder;
        }

        private void Update()
        {
            if (!MovementEnabled)
                return;

            transform.position += _speed * Time.deltaTime * _pathBuilder.GetDirection();
        }
    }
}