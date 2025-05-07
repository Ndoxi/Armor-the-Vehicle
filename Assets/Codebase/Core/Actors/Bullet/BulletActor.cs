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
            _actorStateMachine.EnterState<IdleState>();
        }

        private void OnEnable()
        {
            _attackTrigger.OnEnter += PerformAttack; 
        }

        private void OnDisable()
        {
            _attackTrigger.OnEnter -= PerformAttack;
        }

        public override void HardReset()
        {
            _actorStateMachine.EnterState<IdleState>();
        }

        public override void OnDeath() { }

        public void Fire()
        {
            _actorStateMachine.EnterState<MovementState>();
        }

        private void PerformAttack(Actor actor)
        {
            _attack.Perform(actor);
            _actorStateMachine.EnterState<DeathState>();
        }
    }
}