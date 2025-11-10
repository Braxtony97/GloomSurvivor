using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Services;

namespace GloomSurvivor.Scripts.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>;
        void Enter<TState>() where TState : IState;
    }
}