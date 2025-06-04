using GloomSurvivor.Scripts.Data.SaveLoad;
using GloomSurvivor.Scripts.Services;
using UnityEngine;

namespace GloomSurvivor.Scripts.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        public BoxCollider Collider;
        
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Instance.ResolveSingle<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            
            Debug.Log("Progress Saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!Collider)
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}