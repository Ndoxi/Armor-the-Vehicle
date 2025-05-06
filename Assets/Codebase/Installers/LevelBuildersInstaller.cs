using Zenject;
using Codebase.Core.LevelBuilders;
using UnityEngine;

namespace Codebase.Installers
{
    public class LevelBuildersInstaller : MonoInstaller
    {
        [SerializeField] private Transform _levelStartConnection;

        public override void InstallBindings()
        {
            BindFactory();
            BindLevelBuilder();
        }

        private void BindFactory()
        {
            Container.Bind<LevelBuilderFactory>()
                     .To<LevelBuilderFactory>()
                     .AsSingle();
        }

        private void BindLevelBuilder()
        {
            Container.Bind<LevelBuilder>()
                     .To<LevelBuilder>()
                     .AsSingle()
                     .WithArguments(_levelStartConnection);
        }
    }
}