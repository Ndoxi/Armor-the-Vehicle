using Zenject;

namespace Codebase.Core
{
    public class PreparationsController
    {
        private LazyInject<GameStateMachine> _gameStateMachineLazy;

        public PreparationsController(LazyInject<GameStateMachine> gameStateMachineLazy)
        {
            _gameStateMachineLazy = gameStateMachineLazy;
        }

        public void FinishPreparations()
        {
            _gameStateMachineLazy.Value.EnterState<GameplayLoopState>();
        }
    }
}