using Zenject;
using Codebase.Core;

namespace Codebase.Installers
{
    public class GameRandomInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRandom();
        }

        private void BindRandom()
        {
            Container.Bind<IRandom>()
                     .To<Core.Random>()
                     .AsSingle();
        }
    }
}