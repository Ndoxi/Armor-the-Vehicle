using UnityEngine;
using Zenject;
using Codebase.Core;

namespace Codebase.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CinematicCameraService _cameraService;

        public override void InstallBindings()
        {
            BindMainCamera();
            BindCameraTransitionsManager();
            BindCinematicCameraService();
        }

        private void BindMainCamera()
        {
            Container.Bind<Camera>()
                     .FromInstance(Camera.main);
        }

        private void BindCameraTransitionsManager()
        {
            Container.Bind<CinematicCameraTransitionsManager>()
                     .To<CinematicCameraTransitionsManager>()
                     .AsSingle();
        }

        private void BindCinematicCameraService()
        {
            Container.Bind<CinematicCameraService>()
                     .To<CinematicCameraService>()
                     .FromInstance(_cameraService);
        }
    }
}