namespace Codebase.Core.Actors
{
    public interface IActorState : IState 
    {
        void Update(float deltaTime);
    }
}