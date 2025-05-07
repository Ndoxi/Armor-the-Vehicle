using Codebase.Core.Actors;
using Codebase.Core.LevelBuilders;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Codebase.Core
{
    public class GameplayLoopState : IState
    {
        private const string LogTag = "GameplayLoopState";
        private const int PlayerDollyActivationDelayMs = 2000;

        private readonly LevelBuilder _levelBuilder;
        private readonly ActorsSystem _actorsSystem;
        private readonly CinematicCameraService _cameraService;
        private readonly ILogger _logger;
        private CancellationTokenSource _stateExitCancellationTokenSource;

        public GameplayLoopState(LevelBuilder levelBuilder,
                                 ActorsSystem actorsSystem,
                                 CinematicCameraService cameraService, 
                                 ILogger logger)
        {
            _levelBuilder = levelBuilder;
            _actorsSystem = actorsSystem;
            _cameraService = cameraService;
            _logger = logger;
        }

        public void Enter()
        {
            _stateExitCancellationTokenSource = new CancellationTokenSource();

            _levelBuilder.ActivateChunksLoading(_actorsSystem.PlayerActor);
            _cameraService.SetCameraActive(CinematicCameraService.CinematicCameraType.GameplayStage);
            AllowPlayerMovementWithDelay();
        }

        public void Exit()
        {
            _levelBuilder.DeactivateChunksLoading();
            _levelBuilder.ClearActiveChunks();

            _stateExitCancellationTokenSource?.Cancel();
            _stateExitCancellationTokenSource?.Dispose();

            _actorsSystem.PlayerActor.SetIdle();
        }

        private void AllowPlayerMovementWithDelay()
        {
            AllowPlayerMovementAsync(PlayerDollyActivationDelayMs, _stateExitCancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid AllowPlayerMovementAsync(int delayMs, CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Delay(delayMs, cancellationToken: cancellationToken);
                _actorsSystem.PlayerActor.SetDolly();
            }
            catch (OperationCanceledException)
            {
                _logger.Log(LogTag, "Countdown cancelled");
            }
        }
    }
}