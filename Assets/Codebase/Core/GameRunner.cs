using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Core
{
    public class GameRunner : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _gameStateMachine.EnterState<InitializationState>();
        }
    }
}