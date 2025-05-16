namespace Codebase.Core.Actors
{
    public class PlayerMoveAndShootState : MovementState
    {
        private readonly PlayerActorTurret _turret;
        private readonly ITurretRotationController _rotationController;
        private readonly float _fireInterval = 0.2f;
        private float _timer = 0f;

        public PlayerMoveAndShootState(ActorMovement actorMovement,
                                       IPathBuilder pathBuilder,
                                       PlayerActorTurret turret, 
                                       ITurretRotationController rotationController) : base(actorMovement, pathBuilder)
        {
            _turret = turret;
            _rotationController = rotationController;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _turret.Rotate(_rotationController.GetDirection(), deltaTime);
            _timer -= deltaTime;
            if (_timer <= 0f)
            {
                _turret.Fire();
                _timer = _fireInterval;
            }
        }
    }
}