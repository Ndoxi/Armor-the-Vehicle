using Zenject;
using Codebase.Core.Actors;
using UnityEngine;
using Codebase.Core;

namespace Codebase.Installers
{
    [RequireComponent(typeof(PlayerActor))]
    public class PlayerActorLocalInstaller : MonoInstaller
    {
        [SerializeField] private int _initialHealthValue;

        public override void InstallBindings()
        {
            BindActor();
            BindMovement();
            BindStateMachineFactory();
            BindStateMachine();
            BindPathBuilder();
        }

        private void BindActor()
        {
            Container.Bind<Rigidbody>()
                     .FromInstance(GetComponent<Rigidbody>());

            Container.Bind<ITurretRotationController>()
                     .To<TurretRotationController>()
                     .AsSingle();

            Container.Bind<ActorHealth>()
                     .To<ActorHealth>()
                     .AsSingle()
                     .WithArguments(_initialHealthValue);

            Container.Bind<PlayerActorTurret>()
                     .FromInstance(GetComponent<PlayerActorTurret>());

            Container.Bind(typeof(Actor), typeof(PlayerActor))
                     .FromInstance(GetComponent<PlayerActor>());
        }

        protected void BindMovement()
        {
            Container.Bind<ActorMovement>()
                     .FromInstance(GetComponent<ActorMovement>());
        }

        private void BindStateMachineFactory()
        {
            Container.Bind<ActorStateFactory>()
                     .To<ActorStateFactory>()
                     .AsSingle();
        } 

        private void BindStateMachine()
        {
            Container.Bind<ActorStateMachineBase>()
                     .To<PlayerActorStateMachine>()
                     .AsSingle();
        }

        private void BindPathBuilder()
        {
            Container.Bind<IPathBuilder>()
                     .To<CarPathBuilder>()
                     .AsSingle();
        }
    }
}