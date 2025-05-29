using GloomSurvivor.Scripts.CameraLogic;
using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Services.Input;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;
        
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;
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
    }
}
