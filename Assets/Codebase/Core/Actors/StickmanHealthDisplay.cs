using Zenject;

namespace Codebase.Core.Actors
{
    public class StickmanHealthDisplay : ActorHealthDisplay
    {
        protected override Actor Actor => _actor;
        private StickmanActor _actor;

        [Inject]
        private void Construct(StickmanActor actor)
        {
            _actor = actor;
        }
    }
}