using GloomSurvivor.Scripts.CameraLogic;
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
            _gameFactory = new GameFactory();
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, onLoaded: OnLoaded );
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag("InitialPoint");
            
            var hero = Instantiate("Skeleton_King", initialPoint.transform.position);
            Instantiate("Hud");
            
            CameraFollowHero(hero);

            // GameObject hero = _gameFactory.CreateHero();
            //_gameFactory.CreateHud();
        }

        private GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private GameObject Instantiate(string path, Vector3 point)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, point, Quaternion.identity);
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