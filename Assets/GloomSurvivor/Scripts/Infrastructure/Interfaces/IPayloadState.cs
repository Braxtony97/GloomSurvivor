namespace GloomSurvivor.Scripts.Infrastructure.Interfaces
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}