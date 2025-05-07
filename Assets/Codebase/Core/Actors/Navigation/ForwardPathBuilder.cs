using UnityEngine;

namespace Codebase.Core.Actors
{
    public class ForwardPathBuilder : IPathBuilder
    {
        private readonly Actor _actor;

        public ForwardPathBuilder(Actor actor)
        {
            _actor = actor;
        }

        public Vector3 GetDirection()
        {
            return _actor.transform.forward;
        }
    }
}