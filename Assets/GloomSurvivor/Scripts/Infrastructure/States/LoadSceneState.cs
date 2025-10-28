using Assets.GloomSurvivor.Scripts.UI.Services.Factory;
using GloomSurvivor.Scripts.CameraLogic;
using GloomSurvivor.Scripts.Characters.MainPlayer;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Logic;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using GloomSurvivor.Scripts.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GloomSurvivor.Scripts.Infrastructure.States
{
    public class LoadSceneState : IPayloadState<string>
    {
        private const string Main = "Main";
        private const string EnemySpawnerTag = "EnemySpawner";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            IGameFactory gameFactory, IPersistentProgressService persistentProgressService, IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory; 
        }

        public void Enter(string payload)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(payload, onLoaded: OnLoaded );
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders) 
                progressReader.LoadProgress(_persistentProgressService.Progress);
        }

        private void InitGameWorld()
        {
            LevelStaticData levelData = GetCurrentLevelStaticData();

            InitSpawners(levelData);

            var hero = _gameFactory.CreateHero(levelData.InitialPointHero);
            InitHud(hero);

            _gameFactory.RestoreLoot(_persistentProgressService.Progress);

            CameraFollowHero(hero);
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            hud.GetComponentInChildren<ActorHpBarUI>().
                Construct(hero.GetComponent<IHealth>());
        }

        private void InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawner);
            }
        }

        private LevelStaticData GetCurrentLevelStaticData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.GetLevel(sceneKey);
            return levelData;
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