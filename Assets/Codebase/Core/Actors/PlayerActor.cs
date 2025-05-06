using Zenject;

namespace Codebase.Core.Actors
{
    public class PlayerActor : Actor
    {
        public override ActorStateMachineBase StateMachine => _stateMachine;
        private ActorStateMachineBase _stateMachine;

        [Inject]
        private void Construct(ActorStateMachineBase stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Awake()
        {
            _stateMachine.EnterState<IdleState>();
        }
    }
}