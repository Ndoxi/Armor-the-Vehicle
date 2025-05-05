using Zenject;
using Codebase.Core.Factories;

namespace Codebase.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindViewFactory();
            BindGameStateFactory();
        }

        private void BindViewFactory()
        {
            Container.Bind<GameStateViewFactory>()
                     .To<GameStateViewFactory>()
                     .AsSingle();
        }

        private void BindGameStateFactory()
        {
            Container.Bind<GameStateMachineFactory>()
                     .To<GameStateMachineFactory>()
                     .AsSingle();
        }
    }
}