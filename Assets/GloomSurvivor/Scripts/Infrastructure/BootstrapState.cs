using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using Services.Input;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string BOOT = "Boot";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterService();
        }

        private void RegisterService()
        {
            Game.InputService = RegisterInputService();
            _sceneLoader.Load(BOOT, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadSceneState, string>("Main");

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