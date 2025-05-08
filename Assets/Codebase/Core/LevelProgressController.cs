using Codebase.Core.Actors;
using System;
using UnityEngine;

namespace Codebase.Core
{
    public class LevelProgressController : IDisposable
    {
        public enum State { Completed, Failed }

        public event Action<State> OnFinish;

        public int DistanseToComplete => _distanseToComplete;
        public int CurrentDistanse => _currentDistanse;

        private readonly Actor _playerActor;
        private readonly Vector3 _levelStartPosition;
        private readonly int _distanseToComplete;
        private int _currentDistanse;
        private bool _finished;

        public LevelProgressController(Actor actor, Vector3 levelStartPosition)
        {
            _playerActor = actor;
            _levelStartPosition = levelStartPosition;
            _distanseToComplete = 200;

            _playerActor.OnDeathEvent += OnLevelFailed;
        }

        public void Dispose()
        {
            _playerActor.OnDeathEvent -= OnLevelFailed;
            OnFinish = null;
        }

        public void CheckPlayerProgress()
        {
            if (_finished)
                return;

            _currentDistanse = Mathf.RoundToInt(Vector3.Distance(_levelStartPosition, _playerActor.transform.position));
            if (_currentDistanse >= _distanseToComplete)
                OnLevelCompleted();
        }

        private void OnLevelFailed()
        {
            if (_finished)
                return;

            OnFinish?.Invoke(State.Failed);
            _finished = true;
        }

        private void OnLevelCompleted()
        {
            if (_finished)
                return;

            OnFinish?.Invoke(State.Completed);
            _finished = true;
        }
    }
}