using Codebase.Core.Views;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class PreparationViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAnimatedPanels();
        }

        private void BindAnimatedPanels()
        {
            Container.Bind<IAnimatedPanel>()
                     .FromComponentsInChildren()
                     .AsTransient();
        }
    }
}