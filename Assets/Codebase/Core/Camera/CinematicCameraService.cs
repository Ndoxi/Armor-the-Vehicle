using UnityEngine;
using Cinemachine;
using Zenject;

namespace Codebase.Core
{
    public partial class CinematicCameraService : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _preparationCamera;
        [SerializeField] private CinemachineVirtualCamera _gameplayCamera;
        private CinematicCameraTransitionsManager _cameraTransitionsManager;

        [Inject]
        private void Construct(CinematicCameraTransitionsManager cameraTransitionsManager)
        {
            _cameraTransitionsManager = cameraTransitionsManager;
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
    }
}