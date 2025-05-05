namespace Codebase.Core
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}