using GloomSurvivor.Scripts.Characters.Enemy;
using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Register(ISavedProgressReader progressReader);
        void Cleanup();
        Task<GameObject> CreateMonsterAsync(Enums.MonsterTypeId monsterTypeId, Transform parent);
        LootHandler CreateLoot();
        void RestoreLoot(Progress progress);
        void CreateSpawner(EnemySpawnerData spawner); 
        void CreateLevelTransferSpawner(LevelTransferSpawnerData spawner);
    }
}