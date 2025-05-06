using Codebase.Core.LevelBuilders;
using System;

namespace Codebase.Core
{
    public class GameplayLoopState : IState
    {
        private readonly CinematicCameraService _cameraService;
        private readonly LevelBuilder _levelBuilder;

        public GameplayLoopState(LevelBuilder levelBuilder, CinematicCameraService cameraService)
        {
            _cameraService = cameraService;
            _levelBuilder = levelBuilder;
        }

        public void Enter()
        {
            _cameraService.SetCameraActive(CinematicCameraService.CinematicCameraType.GameplayStage);
            _levelBuilder.Initialize();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}