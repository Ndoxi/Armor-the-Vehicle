using Zenject;
using Codebase.Core.Actors;
using UnityEngine;
using Codebase.Core;

namespace Codebase.Installers
{
    public class ActorsInstaller : MonoInstaller
    {
        [SerializeField] private SwipeInputUI _playerSwipeInput;
        [SerializeField] private Transform _playerSpawnPoint;

        public override void InstallBindings()
        {
            BindFactory();
            BindInput();
            BindSpawner();
            BindBulletSpawner();
            BindActorSystem();
        }

        private void BindFactory()
        {
            Container.Bind<ActorsFactory>()
                     .To<ActorsFactory>()
                     .AsSingle();
        } 
        
        private void BindInput()
        {
            Container.Bind<ISwipeInput>()
                     .FromInstance(_playerSwipeInput)
                     .WhenInjectedInto<TurretRotationController>();
        }

        private void BindSpawner()
        {
            Container.Bind<EnemiesSpawner>()
                     .To<EnemiesSpawner>()
                     .AsSingle();
        }

        private void BindBulletSpawner()
        {
            Container.Bind<BulletSpawner>()
                     .To<BulletSpawner>()
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