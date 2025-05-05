using Zenject;
using Codebase.Core;

namespace Codebase.Installers
{
    public class ResourceLoadersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindDefaultResourceLoader();
        }

        private void BindDefaultResourceLoader()
        {
            Container.Bind<IResourceLoader>()
                     .To<LocalResourcesLoader>()
                     .AsSingle();
        }
    }
}