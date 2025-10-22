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

        public void UpdateProgress(Progress progress) => 
            progress.WorldData.PositionOnLevel = new PositionOnLevel(transform.position.AsVectorData(), CurrentLevel());

        public void LoadProgress(Progress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) 
                    Warp(savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            // TODO: need replace -3
            transform.position = to.AsUnityVector().AddY(_characterController.height).AddX(-3);
            _characterController.enabled = true;
        }

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}
