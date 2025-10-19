using GloomSurvivor.Scripts.Characters.Enemy;
using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;
using static GloomSurvivor.Scripts.Infrastructure.Enums;

namespace GloomSurvivor.Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public bool Slain;
        public MonsterTypeId MonsterTypeId;
        
        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueIdEnemySpawner>().Id;
            _factory = ServiceLocator.Instance.ResolveSingle<IGameFactory>();
        }

        public void LoadProgress(Progress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                Slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
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
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}