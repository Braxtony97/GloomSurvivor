using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Data.SaveLoad;
using GloomSurvivor.Scripts.Logic;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class LootHandler : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private GameObject _boxPrefab;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TMP_Text _lootValueText;
        [SerializeField] private GameObject _pickupPopup;
        
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;
        private string _id;

        private float _lootValueLifetime = 1.5f;
        private ISaveLoadService _saveLoadService;
        private WorldLootItemData _worldLootItemData;

        public void Awake()
        {
            _saveLoadService = ServiceLocator.Instance.ResolveSingle<ISaveLoadService>();
            _id = GetComponent<UniqueIdLoot>().Id;
        }

        public void Construct(WorldData worldData) => 
            _worldData = worldData;

        public void Initialize(Loot loot)
        {
            _loot = loot;

            _worldLootItemData = new WorldLootItemData()
            {
                Id = _id,
                Position = transform.position,
                Value = loot.Value,
                Picked = false
            };

            _worldData.WorldLootData.ActiveLoots.Add(_worldLootItemData);
        }

        public void InitializeWithExistingData(WorldLootItemData worldItemData, Loot loot) 
        {
            _worldLootItemData = worldItemData;
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => 
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;
            _worldLootItemData.Picked = true;

            if (_id == _worldLootItemData.Id)
                _worldData.WorldLootData.ActiveLoots.Remove(_worldLootItemData);


            UpdateWorldData();
            HideBox();
            PlayPickupFx();
            ShowText();

            StartCoroutine(StartDestroyTimer());
        }

        public void UpdateProgress(Progress progress)
        {
            WorldLootItemData itemData = progress.WorldData.WorldLootData.ActiveLoots.Find(x => x.Id == _id);

            if (itemData != null)
            {
                itemData.Picked = _picked;
                itemData.Position = transform.position;
            }
        }

        public void LoadProgress(Progress progress)
        {
            var itemData = progress.WorldData.WorldLootData.ActiveLoots.Find(x => x.Id == _id);
            if (itemData != null)
            {
                if (itemData.Picked)
                    Destroy(gameObject);
                else
                    transform.position = itemData.Position;
            }
        }

        private void UpdateWorldData() => 
            _worldData.LootData.Collect(_loot);

        private void HideBox() => 
            _boxPrefab.SetActive(false);

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(_lootValueLifetime);
            
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