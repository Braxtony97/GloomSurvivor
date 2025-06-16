using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMove _move;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private GameObject _deathEffect;

        private bool _isDead;

        private void Start() =>
            _health.OnHealthChanged += HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && _health.CurrentHP <= 0)
                Die();
        }

        private void Die()
        {
            _move.enabled = false;
            _animator.PlayDeath();
            _isDead = true;
        }

        private void OnDiedAnimationEvent() => 
            Instantiate(_deathEffect, transform.position + transform.forward * 1.3f + transform.up, Quaternion.identity);

        private void OnDestroy() => 
            _health.OnHealthChanged -= HealthChanged;
    }
}