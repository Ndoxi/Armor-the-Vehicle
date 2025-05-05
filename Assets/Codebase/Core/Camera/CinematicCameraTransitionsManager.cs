using Cinemachine;

namespace Codebase.Core
{
    public class CinematicCameraTransitionsManager
    {
        private const int ActiveCameraPriority = 100;
        private const int InactiveCameraPriority = 0;
        private CinemachineVirtualCamera _currentVirtualCamera;

        public void TransitionTo(CinemachineVirtualCamera virtualCamera)
        {
            if (_currentVirtualCamera != null)
                _currentVirtualCamera.Priority = InactiveCameraPriority;

            _currentVirtualCamera = virtualCamera;
            _currentVirtualCamera.Priority = ActiveCameraPriority;
        }
    }
}