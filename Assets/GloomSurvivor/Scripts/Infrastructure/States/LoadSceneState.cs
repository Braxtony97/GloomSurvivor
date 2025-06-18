using GloomSurvivor.Scripts.CameraLogic;
using GloomSurvivor.Scripts.Characters.MainPlayer;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using GloomSurvivor.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GloomSurvivor.Scripts.Infrastructure.States
{
    public class LoadSceneState : IPayloadState<string>
    {
        private const string Main = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(string payload)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(payload, onLoaded: OnLoaded );
        }

        private void OnLoaded()
        {
            InitGameWorld();

            InfoemProgressReaders();
        }

        private void InfoemProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders) 
                progressReader.LoadProgress(_persistentProgressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            var hero = _gameFactory.CreateHero(GameObject.FindWithTag("InitialPoint"));
            var hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorHpBarUI>().Construct(hero.GetComponent<IHealth>());
            
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