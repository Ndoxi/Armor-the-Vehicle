using System;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class PlayerActor : Actor
    {
        public override ActorHealth Health => _actorHealth;

        protected override ActorStateMachineBase StateMachine => _stateMachine;
        protected override Rigidbody Rigidbody => _rigidbody;

        [SerializeField] private ActorHealthDisplay _healthDisplay;
        private ActorStateMachineBase _stateMachine;
        private ActorHealth _actorHealth;
        private PlayerActorTurret _turret;
        private IStateEvent<IActorState> _onEnterStateEvent;
        private IStateEvent<IActorState> _onExitStateEvent;
        private Rigidbody _rigidbody;

        [Inject]
        private void Construct(ActorStateMachineBase stateMachine, 
                               ActorHealth actorHealth, 
                               PlayerActorTurret turret, 
                               Rigidbody rigidbody)
        {
            _stateMachine = stateMachine;
            _actorHealth = actorHealth;
            _turret = turret;
            _onEnterStateEvent = stateMachine.OnEnterEvent;
            _onExitStateEvent = stateMachine.OnExitEvent;
            _rigidbody = rigidbody;
        }

        private void Awake()
        {
            SetInitialState();
        }

        private void OnEnable()
        {
            Health.OnValueChanged += ValidateHealth;
            _onEnterStateEvent.AddListener<PlayerMoveAndShootState>(EnableHealthBar);
            _onExitStateEvent.AddListener<PlayerMoveAndShootState>(DisableHealthBar);
        }

        private void OnDisable()
        {
            Health.OnValueChanged -= ValidateHealth;
            _onEnterStateEvent.RemoveListener<PlayerMoveAndShootState>(EnableHealthBar);
            _onExitStateEvent.RemoveListener<PlayerMoveAndShootState>(DisableHealthBar);
        }

        public void SetDolly()
        {
            _stateMachine.EnterState<PlayerMoveAndShootState>();
        }

        public override void HardReset()
        {
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
            DisableHealthBar();
        }

        private void EnableHealthBar()
        {
            _healthDisplay.gameObject.SetActive(true);
        }

        private void DisableHealthBar()
        {
            _healthDisplay.gameObject.SetActive(false);
        }
    }
}