﻿using Codebase.Core.Actors;
using Codebase.Core.LevelBuilders;
using Codebase.Core.Views;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Codebase.Core
{
    public class GameplayLoopState : IState
    {
        private const string LogTag = "GameplayLoopState";
        private const int PlayerDollyActivationDelayMs = 2000;
        private const int ProgressUpdateIntervalMs = 100;

        private readonly LazyInject<GameStateMachine> _gameStateMachineRef;
        private readonly GameplayLoopStateView _view;
        private readonly LevelBuilder _levelBuilder;
        private readonly ActorsSystem _actorsSystem;
        private readonly CinematicCameraService _cameraService;
        private readonly ILogger _logger;
        private readonly LevelData _levelData;
        private CancellationTokenSource _stateExitCancellationTokenSource;
        private LevelProgressChecker _levelProgress;
        
        public GameplayLoopState(LazyInject<GameStateMachine> gameStateMachineRef, 
                                 GameplayLoopStateView view, 
                                 LevelBuilder levelBuilder,
                                 ActorsSystem actorsSystem,
                                 CinematicCameraService cameraService, 
                                 ILogger logger)
        {
            _gameStateMachineRef = gameStateMachineRef;
            _view = view;
            _levelBuilder = levelBuilder;
            _actorsSystem = actorsSystem;
            _cameraService = cameraService;
            _logger = logger;

            _levelData = new LevelData(300);
        }

        public void Enter()
        {
            var progress = new LevelProgress(_levelData);
            _levelProgress = new LevelProgressChecker(_actorsSystem.PlayerActor, _actorsSystem.PlayerActorSpawnPoint, progress);
            _stateExitCancellationTokenSource = new CancellationTokenSource();
            
            _levelProgress.OnFinish += FinishLevel;
            _view.OnReset += ResetLevel;

            _levelBuilder.ActivateChunksLoading(_actorsSystem.PlayerActor);
            _cameraService.SetCameraActive(CinematicCameraService.CinematicCameraType.GameplayStage);
            UpdatePlayerProgressContinious();
            AllowPlayerMovementWithDelay();

            _view.Show();
            _view.SetProgress(progress);
        }

        public void Exit()
        {
            _view.ResetUI();
            _view.Hide();

            _levelProgress.OnFinish -= FinishLevel;
            _view.OnReset -= ResetLevel;

            _levelProgress.Dispose();
            _levelBuilder.DeactivateChunksLoading();
            _levelBuilder.DespawnChunks();

            _stateExitCancellationTokenSource?.Cancel();
            _stateExitCancellationTokenSource?.Dispose();

            _actorsSystem.Reset();
        }

        private void ResetLevel()
        {
            _gameStateMachineRef.Value.EnterState<PreparationState>();
        }

        private void FinishLevel(LevelProgressChecker.State result)
        {
            _levelBuilder.ClearChunks();
            _view.OnLevelCompleted(result);
            _actorsSystem.PlayerActor.HardReset();
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

        private void UpdatePlayerProgressContinious()
        {
            UpdatePlayerProgressContiniousAsync(ProgressUpdateIntervalMs, _stateExitCancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid UpdatePlayerProgressContiniousAsync(int delay, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Delay(delay, cancellationToken: cancellationToken);
                    _levelProgress.CheckPlayerProgress();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.Log(LogTag, "Countdown cancelled");
            }
        }
    }
}