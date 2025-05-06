using Zenject;
using UnityEngine;

namespace Codebase.Installers
{
    public class UtilityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLogger();
        }

        private void BindLogger()
        {
            Container.Bind<ILogger>().FromInstance(Debug.unityLogger);
        }
    }    
}