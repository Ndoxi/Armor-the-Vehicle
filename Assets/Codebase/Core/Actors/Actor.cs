using UnityEngine;

namespace Codebase.Core.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public abstract ActorStateMachineBase StateMachine { get; }
    }
}