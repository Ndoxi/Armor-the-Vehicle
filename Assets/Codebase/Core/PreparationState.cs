namespace Codebase.Core
{
    public class PreparationState : IState
    {
        private readonly CinematicCameraService _cameraService;
        private readonly IStateView _stateView;

        public PreparationState(CinematicCameraService cameraService, IStateView stateView)
        {
            _cameraService = cameraService;
            _stateView = stateView;
        }

        public void Enter()
        {
            _cameraService.SetCameraActive(CinematicCameraService.CinematicCameraType.PreparationStage);
            _stateView.Show();
        }

        public void Exit()
        {
            _stateView.Hide();
        }
    }
}