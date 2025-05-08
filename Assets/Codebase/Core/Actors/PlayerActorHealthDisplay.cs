using Zenject;

namespace Codebase.Core.Actors
{
    public class PlayerActorHealthDisplay : ActorHealthDisplay
    {
        private IStateEvent<IActorState> _onEnterStateEvent;
        private IStateEvent<IActorState> _onExitStateEvent;

        [Inject]
        private void Construct(ActorStateMachineBase actorStateMachine)
        {
            _onEnterStateEvent = actorStateMachine.OnEnterEvent;
            _onExitStateEvent = actorStateMachine.OnExitEvent;
        }

        protected override void OnEnable()
        {
            DisableHealthBar();
            _onEnterStateEvent.AddListener<PlayerMoveAndShootState>(EnableHealthBar);
            _onExitStateEvent.AddListener<PlayerMoveAndShootState>(DisableHealthBar);
        }

        protected override void OnDisable()
        {
            _onEnterStateEvent.RemoveListener<PlayerMoveAndShootState>(EnableHealthBar);
            _onExitStateEvent.RemoveListener<PlayerMoveAndShootState>(DisableHealthBar);
        }

        private void EnableHealthBar()
        {
            _canvas.enabled = true;
            UpdateDisplay();
            _actor.Health.OnValueChanged += UpdateDisplay;
        }

        private void DisableHealthBar()
        {
            _canvas.enabled = false;
            _actor.Health.OnValueChanged -= UpdateDisplay;
        }
    }
}