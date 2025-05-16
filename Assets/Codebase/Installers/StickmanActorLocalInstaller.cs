using Codebase.Core.Actors;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    [RequireComponent(typeof(StickmanActor))]
    public class StickmanActorLocalInstaller : MonoInstaller
    {
        [SerializeField] private int _initialHealthValue;
        [SerializeField] private int _attackDamage;

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
            Container.Bind<ActorHealth>()
                     .To<ActorHealth>()
                     .AsSingle()
                     .WithArguments(_initialHealthValue);
            Container.Bind<Rigidbody>()
                     .FromInstance(GetComponent<Rigidbody>());

            Container.Bind(typeof(Actor), typeof(StickmanActor))
                     .FromInstance(GetComponent<StickmanActor>());

            Container.Bind<IAttack>()
                     .To<StickmanAttack>()
                     .AsSingle()
                     .WithArguments(_attackDamage);
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
                     .To<StickmanStateMachine>()
                     .AsSingle();
        }

        private void BindPathBuilder()
        {
            Container.Bind(typeof(IPathBuilder), typeof(ChasePathBuilder))
                     .To<ChasePathBuilder>()
                     .AsSingle();
        }
    }
}