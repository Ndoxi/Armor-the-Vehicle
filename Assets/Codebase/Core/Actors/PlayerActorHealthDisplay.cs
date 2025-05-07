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
            base.OnEnable();
            DisableCanvas();
            _onEnterStateEvent.AddListener<MovementState>(EnableCanvas);
            _onExitStateEvent.AddListener<MovementState>(DisableCanvas);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _onEnterStateEvent.RemoveListener<MovementState>(EnableCanvas);
            _onExitStateEvent.RemoveListener<MovementState>(DisableCanvas);
        }

        private void EnableCanvas()
        {
            _canvas.enabled = true;
        }

        private void DisableCanvas()
        {
            _canvas.enabled = false;
        }
    }
}