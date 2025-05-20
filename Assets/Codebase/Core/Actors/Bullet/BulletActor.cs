using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class BulletActor : Actor
    {
        public override ActorHealth Health => null;

        protected override ActorStateMachineBase StateMachine => _actorStateMachine;
        protected override Rigidbody Rigidbody => _rigidbody;

        [SerializeField] private StickmanTrigger _attackTrigger;
        [SerializeField] private TrailRenderer _trailRenderer;

        private ActorStateMachineBase _actorStateMachine;
        private IAttack _attack;
        private Rigidbody _rigidbody;
        private BulletActorVFXController _vfxController;

        [Inject]
        private void Construct(ActorStateMachineBase actorStateMachine,
                               IAttack attack,
                               Rigidbody rigidbody, 
                               BulletActorVFXController vfxController)
        {
            _actorStateMachine = actorStateMachine;
            _attack = attack;
            _rigidbody = rigidbody;
            _vfxController = vfxController;
        }

        private void Awake()
        {
            SetInitialState();
        }

        private void OnEnable()
        {
            _attackTrigger.OnEnterWithContactPoint += PerformAttack;
            _actorStateMachine.OnEnterEvent.AddListener<BulletMovementState>(EnableCollider);
            _actorStateMachine.OnExitEvent.AddListener<BulletMovementState>(DisableCollider);            
            _actorStateMachine.OnEnterEvent.AddListener<BulletMovementState>(EnableTrail);
            _actorStateMachine.OnExitEvent.AddListener<BulletMovementState>(DisableTrail);
        }

        private void OnDisable()
        {
            _attackTrigger.OnEnterWithContactPoint -= PerformAttack;
            _actorStateMachine.OnEnterEvent.RemoveListener<BulletMovementState>(EnableCollider);
            _actorStateMachine.OnExitEvent.RemoveListener<BulletMovementState>(DisableCollider);
            _actorStateMachine.OnEnterEvent.RemoveListener<BulletMovementState>(EnableTrail);
            _actorStateMachine.OnExitEvent.RemoveListener<BulletMovementState>(DisableTrail);
        }

        public override void HardReset()
        {
            SetInitialState();
        }

        public void Fire()
        {
            _actorStateMachine.EnterState<BulletMovementState>();
        }

        private void PerformAttack(Actor target, Vector3 closestPoint)
        {
            _attack.Perform(target);
            PlayHitEffect(target, closestPoint);

            _actorStateMachine.EnterState<DeathState>();
        }

        private void PlayHitEffect(Actor target, Vector3 closestPoint)
        {
            var rotation = Quaternion.LookRotation(target.GetPosition() - closestPoint, Vector3.right);
            _vfxController.Play(closestPoint, rotation);
        }

        private void SetInitialState()
        {
            _actorStateMachine.EnterState<IdleState>();
            DisableTrail();
        }

        private void EnableCollider()
        {
            _attackTrigger.Collider.enabled = true;
        }

        private void DisableCollider()
        {
            _attackTrigger.Collider.enabled = false;
        }

        private void EnableTrail()
        {
            _trailRenderer.Clear();
            _trailRenderer.emitting = true;
        }

        private void DisableTrail()
        {
            _trailRenderer.emitting = false;
            _trailRenderer.Clear();
        }
    }
}