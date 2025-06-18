using System;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;
        
        [SerializeField] private EnemyAnimator _enemyAnimator;
        
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            
            _enemyAnimator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}