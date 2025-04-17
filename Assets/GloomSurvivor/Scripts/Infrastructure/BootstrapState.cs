using Infrastructure;
using Services.Input;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        { 
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            RegisterService();
        }

        private void RegisterService()
        {
            Game.InputService = RegisterInputService();
        }

        public void Exit()
        {
        }

        private static IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        
    }
}