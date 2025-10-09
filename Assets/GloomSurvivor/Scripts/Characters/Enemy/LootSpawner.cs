using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory) => 
            _factory = factory;

        private void Start() => 
            _enemyDeath.Died += SpawnLoot;

        public void SetLoot(int minLoot, int maxLoot)
        {
            _lootMin = minLoot;
            _lootMax = maxLoot;
        }

        private void SpawnLoot()
        {
            LootHandler lootHandler = _factory.CreateLoot();
            lootHandler.transform.position = transform.position;

            Loot lootItem = SetValue();
            lootHandler.Initialize(lootItem);
        }

        private Loot SetValue() => 
            new() { Value = Random.Range(_lootMin, _lootMax) };

        private void OnDestroy() =>
            _enemyDeath.Died -= SpawnLoot;
    }
}