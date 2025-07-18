using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class RotateToPlayer : EnemyFollow
    {
        private float _speed = 5f;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;
        
        public void Construct(GameObject heroGameObject) => 
            _heroTransform = heroGameObject.transform;

        private void Update()
        {
            if (_heroTransform != null)
                RotateTowardsHero();
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();
            
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private Quaternion SmoothedRotation(Quaternion transformRotation, Vector3 positionToLook) => 
            Quaternion.Lerp(transformRotation, TargetRotation(positionToLook), SpeedFactor());

        private float SpeedFactor() => 
            _speed * Time.deltaTime;

        private Quaternion TargetRotation(Vector3 positionToLook) => 
            Quaternion.LookRotation(positionToLook);

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }
    }
}