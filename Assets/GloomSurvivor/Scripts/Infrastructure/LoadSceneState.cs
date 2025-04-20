using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public class LoadSceneState : IPayloadState<string>
    {
        private const string Main = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload);
        }

        public void Exit()
        {
        }
    }
}