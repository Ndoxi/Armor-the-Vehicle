using Zenject;
using UnityEngine;
using System;

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
            Container.Bind<ILogger>()
                     .FromInstance(Debug.unityLogger);
        }
    }
}