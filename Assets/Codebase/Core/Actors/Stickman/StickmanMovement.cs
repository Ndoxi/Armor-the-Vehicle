using UnityEngine;

namespace Codebase.Core.Actors
{
    [RequireComponent(typeof(ActorAnimator))]
    public class StickmanMovement : ActorMovement
    {
        [SerializeField] private float _rotationSpeed;
        private ActorAnimator _actorAnimator;

        private void Awake()
        {
            _actorAnimator = GetComponent<ActorAnimator>();
        }

        public override void MoveTowards(Vector3 direction, float deltaTime)
        {
            base.MoveTowards(direction, deltaTime);

            LookAt(direction, deltaTime);
            _actorAnimator.SetSpeed(_speed * deltaTime);
        }

        private void LookAt(Vector3 direction, float deltaTime)
        {
            var flatDirection = new Vector3(direction.x , 0, direction.z);
            var targetRotation = Quaternion.LookRotation(flatDirection);
            _rigidbody.rotation = Quaternion.Slerp(transform.rotation, targetRotation, deltaTime * _rotationSpeed);
        }
    }
}