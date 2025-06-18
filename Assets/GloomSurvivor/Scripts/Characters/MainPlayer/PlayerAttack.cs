using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.Input;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private CharacterController _characterController;
        
        private IInputService _inputService;
        private Collider[] _hits = new Collider[3];
        private PlayerStats _playerStats;

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

        public void OnAttack_Normal()
        {
            for (int i = 0; i < _hits.Length; i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_playerStats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress playerProgress) => 
            _playerStats = playerProgress.PlayerStats;

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _playerStats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
    }
}