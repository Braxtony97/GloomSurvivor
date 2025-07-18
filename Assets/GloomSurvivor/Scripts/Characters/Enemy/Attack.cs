using System.Linq;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public float Damage = 10f;
        public float EffectiveDistance = 0.5f;
        
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private float _attackCooldown = 3f;

        private IGameFactory _factory;
        private Transform _heroTransform;

        private float _currentCooldown;
        private bool _isAttacking;
        private float _radius = 0.5f;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private bool _attackIsActive;
        

        public void Construct(GameObject heroGameObject) => 
            _heroTransform = heroGameObject.transform;

        private void Awake() => 
            _layerMask = 1 << LayerMask.NameToLayer("Player");

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                if (hit.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    PhysicsDebug.DrawDebug(StartPoint(), _radius, 1f);
                    hit.transform.GetComponent<IHealth>().TakeDamage(Damage); // TEMP
                }
            }
        }

        private bool Hit(out Collider hit)
        {
            var hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _radius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private Vector3 StartPoint() => 
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;

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
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() => 
            _currentCooldown <= 0f;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _enemyAnimator.PlayDefaultAttack();

            _isAttacking = true;
        }
    }
}