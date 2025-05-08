using System;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class PlayerActor : Actor
    {
        protected override ActorStateMachineBase StateMachine => _stateMachine;

        public override ActorHealth Health => _actorHealth;

        private ActorStateMachineBase _stateMachine;
        private ActorHealth _actorHealth;
        private PlayerActorTurret _turret;

        [Inject]
        private void Construct(ActorStateMachineBase stateMachine, 
                               ActorHealth actorHealth, 
                               PlayerActorTurret turret)
        {
            _stateMachine = stateMachine;
            _actorHealth = actorHealth;
            _turret = turret;
        }

        private void Awake()
        {
            SetInitialState();
        }

        private void OnEnable()
        {
            Health.OnValueChanged += ValidateHealth;
        }

        private void OnDisable()
        {
            Health.OnValueChanged -= ValidateHealth;
        }

        public void SetDolly()
        {
            _stateMachine.EnterState<PlayerMoveAndShootState>();
        }

        public override void OnDeath()
        {
            base.OnDeath();
            _stateMachine.EnterState<DeathState>();
        }

        public override void HardReset()
        {
            base.HardReset();
            SetInitialState();
        }

        private void ValidateHealth(int value)
        {
            if (value > 0)
                return;
            _stateMachine.EnterState<DeathState>();
        }

        private void SetInitialState()
        {
            _stateMachine.EnterState<IdleState>();
            _actorHealth.Increase(_actorHealth.Initial - _actorHealth.Current);
            _turret.HardReset();
        }
    }
}