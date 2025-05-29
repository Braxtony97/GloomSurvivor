namespace GloomSurvivor.Scripts.Infrastructure.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}