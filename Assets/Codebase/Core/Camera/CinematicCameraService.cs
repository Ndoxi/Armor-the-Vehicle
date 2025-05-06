using UnityEngine;
using Cinemachine;
using Zenject;
using Codebase.Core.Actors;

namespace Codebase.Core
{
    public partial class CinematicCameraService : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _preparationCamera;
        [SerializeField] private CinemachineVirtualCamera _gameplayCamera;
        private CinematicCameraTransitionsManager _cameraTransitionsManager;
        private ActorsSystem _actorsSystem;

        [Inject]
        private void Construct(CinematicCameraTransitionsManager cameraTransitionsManager,
                               ActorsSystem actorsSystem)
        {
            _cameraTransitionsManager = cameraTransitionsManager;
            _actorsSystem = actorsSystem;
        }

        private void Awake()
        {
            SetActorToFollow(_gameplayCamera, _actorsSystem.PlayerActor);
        }

        public void SetCameraActive(CinematicCameraType cameraType)
        {
            _cameraTransitionsManager.TransitionTo(GetCamera(cameraType));
        }

        private CinemachineVirtualCamera GetCamera(CinematicCameraType cameraType)
        {
            return cameraType switch
            {
                CinematicCameraType.PreparationStage => _preparationCamera,
                CinematicCameraType.GameplayStage => _gameplayCamera,
                _ => throw new System.ArgumentException($"Camera of type {cameraType} not found"),
            };
        }

        private void SetActorToFollow(CinemachineVirtualCamera virtualCamera, Actor actor)
        {
            virtualCamera.Follow = actor.transform;
        }
    }
}