using UnityEngine;

namespace GloomSurvivor.Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _followingObject;
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;

        private void LateUpdate()
        {
            if (_followingObject == null)
                return;

            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPosition();

            transform.position = position;
            transform.rotation = rotation;
        }

        public void Follow(GameObject target) => _followingObject = target.transform;

        private Vector3 FollowingPosition()
        {
            Vector3 followingPosition = _followingObject.position;
            followingPosition.y = _offsetY;
            return followingPosition;
        }
    }
}