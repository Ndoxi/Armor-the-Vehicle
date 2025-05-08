using Zenject;

namespace Codebase.Core
{
    public class PreparationsController
    {
        private readonly LazyInject<GameStateMachine> _gameStateMachineLazy;

        public PreparationsController(LazyInject<GameStateMachine> gameStateMachineRef)
        {
            _gameStateMachineLazy = gameStateMachineRef;
        }

        public void FinishPreparations()
        {
            _gameStateMachineLazy.Value.EnterState<GameplayLoopState>();
        }
    }
}