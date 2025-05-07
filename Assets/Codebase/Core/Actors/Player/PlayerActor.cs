using System;
using Zenject;

namespace Codebase.Core.Actors
{
    public class PlayerActor : Actor
    {
        protected override ActorStateMachineBase StateMachine => _stateMachine;

        public override ActorHealth Health => _actorHealth;

        private ActorStateMachineBase _stateMachine;
        private ActorHealth _actorHealth;

        [Inject]
        private void Construct(ActorStateMachineBase stateMachine, 
                               ActorHealth actorHealth)
        {
            _stateMachine = stateMachine;
            _actorHealth = actorHealth;
        }

        private void Awake()
        {
            _stateMachine.EnterState<IdleState>();
        }

        public void SetIdle()
        {
            _stateMachine.EnterState<IdleState>();
        }

        public void SetDolly()
        {
            _stateMachine.EnterState<MovementState>();
        }

        public override void OnDeath()
        {
            _stateMachine.EnterState<IdleState>();
        }

        public override void HardReset()
        {
            _stateMachine.EnterState<IdleState>();
            _actorHealth.Increase(_actorHealth.Initial - _actorHealth.Current);
        }
    }
}