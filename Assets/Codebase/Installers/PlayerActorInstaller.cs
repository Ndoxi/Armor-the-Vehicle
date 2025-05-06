using Zenject;
using Codebase.Core.Actors;
using UnityEngine;

namespace Codebase.Installers
{
    [RequireComponent(typeof(PlayerActor))]
    public class PlayerActorInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            BindActor();
            BindStateMachineFactory();
            BindStateMachine();
            BindPathBuilder();
        }

        private void BindActor()
        {
            Container.Bind<Actor>()
                     .FromInstance(GetComponent<PlayerActor>());
        }

        private void BindStateMachineFactory()
        {
            Container.Bind<PlayerActorStateFactory>()
                     .To<PlayerActorStateFactory>()
                     .AsTransient();
        } 

        private void BindStateMachine()
        {
            Container.Bind<ActorStateMachineBase>()
                     .To<PlayerActorStateMachine>()
                     .AsTransient();
        }

        private void BindPathBuilder()
        {
            Container.Bind<IPathBuilder>()
                     .To<DollyPathBuilder>()
                     .AsTransient();
        }
    }
}