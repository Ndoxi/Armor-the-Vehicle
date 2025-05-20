using Zenject;
using Codebase.Core.Actors;
using System;

namespace Codebase.Installers
{
    public class ActorsVFXInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBulletActorVFXController();
            BindStickmanActorVFXController();
        }

        private void BindBulletActorVFXController()
        {
            var controller = Container.Instantiate<BulletActorVFXController>();
            controller.Initialize();

            Container.Bind(typeof(BulletActorVFXController), typeof(IDisposable))
                     .FromInstance(controller);
        }        
        
        private void BindStickmanActorVFXController()
        {
            var controller = Container.Instantiate<StickmanActorVFXController>();
            controller.Initialize();

            Container.Bind(typeof(StickmanActorVFXController), typeof(IDisposable))
                     .FromInstance(controller);
        }
    }
}