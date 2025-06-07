using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class AgentMoveToPlayer : EnemyFollow
    {
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        private float _minDistance = 1;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = ServiceLocator.Instance.ResolveSingle<IGameFactory>(); // Temp

            if (_gameFactory.HeroGameObject != null) 
                InitializeHero();
            else
                _gameFactory.HeroCreated += HeroCreated;
        }

        private void Update()
        {
            if (_heroTransform != null && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private void HeroCreated() => 
            InitializeHero();

        private void InitializeHero() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >=  _minDistance;

        private void OnDestroy() => 
            _gameFactory.HeroCreated -= HeroCreated;
    }
}