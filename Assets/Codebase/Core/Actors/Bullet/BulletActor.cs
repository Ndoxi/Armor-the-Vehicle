using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class BulletActor : Actor
    {
        public override ActorHealth Health => null;
        [SerializeField] private StickmanTrigger _attackTrigger;
        protected override ActorStateMachineBase StateMachine => _actorStateMachine;
        private ActorStateMachineBase _actorStateMachine;
        private IAttack _attack;

        [Inject]
        private void Construct(ActorStateMachineBase actorStateMachine, IAttack attack)
        {
            _actorStateMachine = actorStateMachine;
            _attack = attack;
        }

        private void Awake()
        {
            SetInitialState();
        }

        private void OnEnable()
        {
            _attackTrigger.OnEnter += PerformAttack;
            _actorStateMachine.OnEnterEvent.AddListener<BulletMovementState>(EnableCollider);
            _actorStateMachine.OnExitEvent.AddListener<BulletMovementState>(DisableCollider);
        }

        private void OnDisable()
        {
            _attackTrigger.OnEnter -= PerformAttack;
            _actorStateMachine.OnEnterEvent.RemoveListener<BulletMovementState>(EnableCollider);
            _actorStateMachine.OnExitEvent.RemoveListener<BulletMovementState>(DisableCollider);
        }

        public override void HardReset()
        {
            base.HardReset();
            SetInitialState();
        }

        public void Fire()
        {
            _actorStateMachine.EnterState<BulletMovementState>();
        }

        private void PerformAttack(Actor actor)
        {
            _attack.Perform(actor);
            _actorStateMachine.EnterState<DeathState>();
        }

        private void SetInitialState()
        {
            _actorStateMachine.EnterState<IdleState>();
        }

        private void EnableCollider()
        {
            _attackTrigger.Collider.enabled = true;
        }

        private void DisableCollider()
        {
            _attackTrigger.Collider.enabled = true;
        }
    }
}