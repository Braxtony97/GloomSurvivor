using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public class LoadSceneState : IPayloadState<string>
    {
        private const string Main = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = new GameFactory();
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, onLoaded: OnLoaded );
        }

        private void OnLoaded()
        {
           // GameObject hero = _gameFactory.CreateHero();
            _gameFactory.CreateHud(); 
        }

        public void Exit()
        {
        }
    }
}