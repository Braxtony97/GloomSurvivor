using System;
using System.Collections;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
         public event Action Died;
        
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathEffect;

        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.CurrentHealth <= 0)
            {
                Die();
            }    
        }

        private void Die()
        {
            _health.HealthChanged -= HealthChanged;
            
            _animator.PlayDeath();
            Instantiate(_deathEffect, transform.position, Quaternion.identity);

            StartCoroutine(DestroyTimer());
            
            Died?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

        private void OnDestroy() => 
            _health.HealthChanged -= HealthChanged;
    }
}