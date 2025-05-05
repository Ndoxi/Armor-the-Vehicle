using Zenject;
using Codebase.Core;

namespace Codebase.Installers
{
    public class GameControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPreparationsController();
        }

        private void BindPreparationsController()
        {
            Container.Bind<PreparationsController>()
                     .To<PreparationsController>()
                     .AsSingle();
        }
    }
}