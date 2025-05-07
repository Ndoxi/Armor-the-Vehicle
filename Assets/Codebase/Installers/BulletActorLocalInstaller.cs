using Zenject;
using Codebase.Core.Actors;
using UnityEngine;

namespace Codebase.Installers
{
    [RequireComponent(typeof(BulletActor))]
    public class BulletActorLocalInstaller : MonoInstaller
    {
        [SerializeField] private int _damage;

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
            Container.Bind<IAttack>()
                     .To<BulletAttack>()
                     .AsSingle()
                     .WithArguments(_damage);

            Container.Bind(typeof(Actor), typeof(BulletActor))
                     .FromInstance(GetComponent<BulletActor>());
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
                     .AsTransient();
        }

        private void BindStateMachine()
        {
            Container.Bind<ActorStateMachineBase>()
                     .To<BulletActorStateMachine>()
                     .AsSingle();
        }

        private void BindPathBuilder()
        {
            Container.Bind<IPathBuilder>()
                     .To<ForwardPathBuilder>()
                     .AsSingle();
        }
    }
}