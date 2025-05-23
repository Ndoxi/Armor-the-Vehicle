﻿using Codebase.Core.Actors;
using Codebase.Core.LevelBuilders;
using Zenject;

namespace Codebase.Core
{
    public class InitializationState : IState
    {
        private GameStateMachine GameStateMachine => _gameStateMachineRef.Value;
        private readonly LevelBuilder _levelBuilder;
        private readonly EnemiesSpawner _spawner;
        private readonly BulletSpawner _bulletSpawner;
        private readonly ActorsSystem _actorsSystem;
        private readonly LazyInject<GameStateMachine> _gameStateMachineRef;

        public InitializationState(LevelBuilder levelBuilder,
                                   EnemiesSpawner spawner, 
                                   BulletSpawner bulletSpawner,
                                   ActorsSystem actorsSystem,
                                   LazyInject<GameStateMachine> gameStateMachineRef)
        {
            _levelBuilder = levelBuilder;
            _spawner = spawner;
            _bulletSpawner = bulletSpawner;
            _actorsSystem = actorsSystem;
            _gameStateMachineRef = gameStateMachineRef;
        }

        public void Enter()
        {
            _levelBuilder.Initialize();
            _spawner.Initialize();
            _bulletSpawner.Initialize();
            _actorsSystem.CreatePlayer();
            GameStateMachine.EnterState<PreparationState>();
        }

        public void Exit()
        {
        }
    }
}