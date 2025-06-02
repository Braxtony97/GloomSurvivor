using GloomSurvivor.Scripts.CameraLogic;
using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.Input;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    public class PlayerMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;
        
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = ServiceLocator.Instance.ResolveSingle<IInputService>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress playerProgress) => 
            playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(transform.position.AsVectorData(), CurrentLevel());

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (CurrentLevel() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = playerProgress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) 
                    Warp(to: savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector();
            _characterController.enabled = true;
        }

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}
