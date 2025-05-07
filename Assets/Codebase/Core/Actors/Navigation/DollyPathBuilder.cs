using UnityEngine;

namespace Codebase.Core.Actors
{
    public class DollyPathBuilder : IPathBuilder
    {
        private readonly Actor _actor;

        public DollyPathBuilder(Actor actor)
        {
            _actor = actor;
        }

        public Vector3 GetDirection()
        {
            return _actor.transform.forward;
        }
    }
}