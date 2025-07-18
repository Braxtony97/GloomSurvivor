using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class AgentMoveToPlayer : EnemyFollow
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _minDistance = 2f;

        private Transform _heroTransform;

        private IGameFactory _gameFactory;

        public void Construct(GameObject heroGameObject) => 
            _heroTransform = heroGameObject.transform;

        private void Update()
        {
            if (_heroTransform != null && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >=  _minDistance;
    }
}