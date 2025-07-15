using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Serialization;

namespace GloomSurvivor.Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public bool Slain;
        public Enums.MonsterTypeId MonsterTypeId;
        
        private string _id;

        private void Awake()
        {
            _id = GetComponent<UniqueIdEnemySpawner>().Id;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
                Slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if(Slain)
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}