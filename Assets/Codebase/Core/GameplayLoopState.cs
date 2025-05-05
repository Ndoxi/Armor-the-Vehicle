using System;

namespace Codebase.Core
{
    public class GameplayLoopState : IState
    {
        private readonly CinematicCameraService _cameraService;

        public GameplayLoopState(CinematicCameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void Enter()
        {
            _cameraService.SetCameraActive(CinematicCameraService.CinematicCameraType.GameplayStage);
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}