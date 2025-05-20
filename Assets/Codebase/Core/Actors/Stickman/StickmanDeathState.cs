namespace Codebase.Core.Actors
{
    public class StickmanDeathState : DeathState
    {
        private readonly ActorVFXController _vfxController;

        public StickmanDeathState(Actor parent, StickmanActorVFXController vfxController) : base(parent)
        {
            _vfxController = vfxController;
        }

        public override void Enter()
        {
            _vfxController.Play(_parent.GetPosition(), _parent.GetRotation());
            base.Enter();
        }
    }
}