using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public abstract class ActorStateMachineBase : StateMachine<IActorState>
    {
        public ActorStateMachineBase(ILogger logger) : base(logger) { }
    }
}