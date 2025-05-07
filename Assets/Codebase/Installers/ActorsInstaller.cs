using Zenject;
using Codebase.Core.Actors;
using UnityEngine;

namespace Codebase.Installers
{
    public class ActorsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerSpawnPoint;

        public override void InstallBindings()
        {
            BindFactory();
            BindSpawner();
            BindActorSystem();
        }

        private void BindFactory()
        {
            Container.Bind<ActorsFactory>()
                     .To<ActorsFactory>()
                     .AsSingle();
        }  
        
        private void BindSpawner()
        {
            Container.Bind<EnemiesSpawner>()
                     .To<EnemiesSpawner>()
                     .AsSingle();
        }
        
        private void BindActorSystem()
        {
            Container.Bind<ActorsSystem>()
                     .To<ActorsSystem>()
                     .AsSingle()
                     .WithArguments(_playerSpawnPoint);
        }
    }
}