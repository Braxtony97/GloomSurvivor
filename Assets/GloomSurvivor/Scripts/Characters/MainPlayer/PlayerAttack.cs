using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.Input;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private CharacterController _characterController;
        
        private IInputService _inputService;
        
        private static int _layerMask;

        private void Awake()
        {
            _inputService = ServiceLocator.Instance.ResolveSingle<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsAttackButton() && !_playerAnimator.IsAttacking)
            {
                _playerAnimator.PlayAttack();
            }
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
    }
}