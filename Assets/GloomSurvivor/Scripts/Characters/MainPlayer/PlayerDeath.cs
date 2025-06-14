using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
            
            //Instantiate(_deathEffect, transform.position, Quaternion.identity);
            
            _isDead = true;
        }

        private void OnDestroy() => 
            _health.OnHealthChanged -= HealthChanged;
    }
}