using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Infrastructure.States;
using GloomSurvivor.Scripts.Services;
using UnityEngine;

namespace Assets.GloomSurvivor.Scripts.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        public Enums.Scenes Scene;

        private const string PlayerTag = "Player";
        private IGameStateMachine _gameStateMachine;
        private bool _triggered;

        public void Start()
        {
            _gameStateMachine = ServiceLocator.Instance.ResolveSingle<IGameStateMachine>(); //TEMP
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
