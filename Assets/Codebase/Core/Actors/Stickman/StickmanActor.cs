using System;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class StickmanActor : Actor
    {
        public override ActorHealth Health => _actorHealth;

        protected override ActorStateMachineBase StateMachine => _stateMachine;
        protected override Rigidbody Rigidbody => _rigidbody;

        [SerializeField] private PlayerTrigger _detectionTrigger;
        [SerializeField] private PlayerTrigger _attackTrigger;
        private ActorStateMachineBase _stateMachine;
        private ActorHealth _actorHealth;
        private ChasePathBuilder _pathBuilder;
        private IAttack _attack;
        private Rigidbody _rigidbody;
        private StickmanActorVFXController _vfxController;

        [Inject]
        private void Construct(ActorStateMachineBase stateMachine,
                               ActorHealth actorHealth,
                               ChasePathBuilder pathBuilder,
                               IAttack attack, 
                               Rigidbody rigidbody, 
                               StickmanActorVFXController vfxController)
        {
            _stateMachine = stateMachine;
            _actorHealth = actorHealth;
            _pathBuilder = pathBuilder;
            _attack = attack;
            _rigidbody = rigidbody;
            _vfxController = vfxController;
        }

        private void Awake()
        {
            _stateMachine.EnterState<IdleState>();
        }

        private void OnEnable()
        {
            _detectionTrigger.OnEnter += EnterChaseState;
            _attackTrigger.OnEnter += PerformAttack;
            _actorHealth.OnValueChanged += ValidateHealth;
        }

        private void OnDisable()
        {
            _detectionTrigger.OnEnter -= EnterChaseState;
            _attackTrigger.OnEnter -= PerformAttack;
            _actorHealth.OnValueChanged -= ValidateHealth;
        }

        private void EnterChaseState(PlayerActor playerActor)
        {
            _pathBuilder.Follow(playerActor);
            _stateMachine.EnterState<MovementState>();
        }

        private void ValidateHealth(int value)
        {
            if (value > 0)
                return;
            _stateMachine.EnterState<StickmanDeathState>();
        }

        private void PerformAttack(PlayerActor playerActor)
        {
            _attack.Perform(playerActor);
        }

        public override void HardReset()
        {
            _stateMachine.EnterState<IdleState>();
            _actorHealth.Increase(_actorHealth.Initial - _actorHealth.Current);
        }
    }
}