using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<Enums.MonsterTypeId, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterType, x => x);
        }

        public MonsterStaticData GetMonster(Enums.MonsterTypeId monsterId) => 
            _monsters.TryGetValue(monsterId, out MonsterStaticData monster) 
                ? monster 
                : null;
    }
}