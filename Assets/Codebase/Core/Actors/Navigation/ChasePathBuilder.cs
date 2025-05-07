using UnityEngine;

namespace Codebase.Core.Actors
{
    public class ChasePathBuilder : IPathBuilder
    {
        private readonly Actor _parent;
        private Actor _target;

        public ChasePathBuilder(Actor parent)
        {
            _parent = parent;
        }

        public void Follow(Actor actor)
        {
            _target = actor;
        }

        public Vector3 GetDirection()
        {
            if (_target == null)
                return Vector3.zero;
            return (_target.transform.position - _parent.transform.position).normalized;
        }
    }
}