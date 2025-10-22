using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<Enums.MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterType, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData GetMonster(Enums.MonsterTypeId monsterId) => 
            _monsters.TryGetValue(monsterId, out MonsterStaticData monsterData) 
                ? monsterData
                : null;

        public LevelStaticData GetLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData levelData)
                ? levelData
                : null;
    }
}