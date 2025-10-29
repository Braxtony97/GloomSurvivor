using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Infrastructure.States;
using GloomSurvivor.Scripts.Services;
using UnityEngine;

namespace Assets.GloomSurvivor.Scripts.Logic.LevelTransferTriggerSpawner
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [HideInInspector] public Enums.Scenes Scene;
        [HideInInspector] public string Id;

        private const string PlayerTag = "Player";
        private IGameStateMachine _gameStateMachine;
        private bool _triggered;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void OnTriggerEnter(Collider other)  
        {

            if (_triggered)
                return;

            if (other.CompareTag(PlayerTag))
            {
                _gameStateMachine.Enter<LoadSceneState, string>(Scene.ToString());
                _triggered = true;
            }
        }
    }
}
