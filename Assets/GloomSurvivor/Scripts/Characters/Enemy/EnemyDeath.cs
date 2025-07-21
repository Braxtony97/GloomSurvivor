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
        
        [Header("Death effect settings")]
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private float _deathEffectScale = 2.5f;
        [SerializeField] private float _offsetDistance = 1f;
        
        private GameObject _spawnedDeathEffect;

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
            _spawnedDeathEffect = Instantiate(_deathEffect, transform.position - transform.forward * _offsetDistance, Quaternion.identity);
            _spawnedDeathEffect.transform.localScale *= _deathEffectScale;

            StartCoroutine(DestroyTimer());
            
            Died?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
            
            if (_spawnedDeathEffect != null)
                Destroy(_spawnedDeathEffect);
        }
    }
}