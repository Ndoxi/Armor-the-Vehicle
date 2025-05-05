using Zenject;
using Codebase.Core;

namespace Codebase.Installers
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>()
                     .To<GameStateMachine>()
                     .AsSingle();
        }
    }
}