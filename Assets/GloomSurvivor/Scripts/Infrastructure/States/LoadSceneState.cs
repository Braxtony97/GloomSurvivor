using GloomSurvivor.Scripts.CameraLogic;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GloomSurvivor.Scripts.Infrastructure.States
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
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, onLoaded: OnLoaded );
        }

        private void OnLoaded()
        {
            var hero = _gameFactory.CreateHero(GameObject.FindWithTag("InitialPoint"));
            _gameFactory.CreateHud();
            
            CameraFollowHero(hero);
        }

        private void CameraFollowHero(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }

        public void Exit()
        {
        }
    }
}