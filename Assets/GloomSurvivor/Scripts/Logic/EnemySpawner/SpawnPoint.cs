using GloomSurvivor.Scripts.Characters.Enemy;
using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;
using static GloomSurvivor.Scripts.Infrastructure.Enums;

namespace GloomSurvivor.Scripts.Logic
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public bool Slain;
        public MonsterTypeId MonsterTypeId; 
        public string Id { get; set; }

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory gameFactory) => 
            _factory = gameFactory;

        public void LoadProgress(Progress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                Slain = true;
            else
                Spawn();
        }

        private async void Spawn()
        {
            GameObject monster = await _factory.CreateMonsterAsync(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Died += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Died -= Slay;
            
            Slain = true;
        }

        public void UpdateProgress(Progress progress)
        {
            if(Slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }
    }
}