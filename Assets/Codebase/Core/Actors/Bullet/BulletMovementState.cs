using Zenject;

namespace Codebase.Core.Actors
{
    public class BulletMovementState : MovementState
    {
        private readonly LazyInject<ActorStateMachineBase> _stateMachineRef;
        private readonly float _lifetime;
        private float _lifespan;

        public BulletMovementState(LazyInject<ActorStateMachineBase> stateMachineRef,
                                   ActorMovement actorMovement,
                                   IPathBuilder pathBuilder) : base(actorMovement, pathBuilder)
        {
            _stateMachineRef = stateMachineRef;
            _lifetime = 5f;
        }

        public override void Enter()
        {
            base.Enter();
            _lifespan = 0;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            _lifespan += deltaTime;
            if (_lifespan >= _lifetime)
            {
                KillBullet();
                _lifespan = 0;
            }
        }

        private void KillBullet()
        {
            _stateMachineRef.Value.EnterState<DeathState>();
        }
    }
}