using UnityEngine;

namespace Codebase.Core.Actors
{
    public class CarPathBuilder : IPathBuilder
    {
        private readonly Actor _actor;

        public CarPathBuilder(Actor actor)
        {
            _actor = actor;
        }

        public Vector3 GetDirection()
        {
            return _actor.transform.forward;
        }
    }
}