using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _attackCooldown = 3f;
        private float _currentCooldown;
        private bool _isAttacking;

        private void Awake()
        {
            _factory = ServiceLocator.Instance.ResolveSingle<IGameFactory>();
            _factory.HeroCreated += OnHeroCreated; // Temp
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
        }

        private void OnAttackEnded()
        {
            _currentCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentCooldown -= Time.deltaTime;
        }

        private bool CanAttack() => 
            _isAttacking && CooldownIsUp();

        private bool CooldownIsUp() => 
            _currentCooldown <= 0f;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _enemyAnimator.PlayDefaultAttack();

            _isAttacking = true;
        }

        private void OnHeroCreated() => 
            _heroTransform = _factory.HeroGameObject.transform;
    }
}