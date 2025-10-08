using System.Collections;
using GloomSurvivor.Scripts.Data;
using TMPro;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class LootHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _boxPrefab;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TMP_Text _lootValueText;
        [SerializeField] private GameObject _pickupPopup;
        
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData) => 
            _worldData = worldData;

        public void Initialize(Loot loot) => 
            _loot = loot;

        private void OnTriggerEnter(Collider other) => 
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;
            
            UpdateWorldData();
            HideBox();
            PlayPickupFx();
            ShowText();

            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() => 
            _worldData.LootData.Collect(_loot);

        private void HideBox() => 
            _boxPrefab.SetActive(false);

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(2f);
            
            Destroy(gameObject);
        }

        private void PlayPickupFx() => 
            Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            _lootValueText.text = $"{_loot.Value}";
            _pickupPopup.SetActive(true);
        }
    }
}