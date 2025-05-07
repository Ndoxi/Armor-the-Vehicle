using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class ActorMovement : MonoBehaviour
    {
        [SerializeField] protected float _speed;

        public virtual void MoveTowards(Vector3 direction, float deltaTime)
        {
            transform.position += _speed * deltaTime * direction;
        }
    }
}