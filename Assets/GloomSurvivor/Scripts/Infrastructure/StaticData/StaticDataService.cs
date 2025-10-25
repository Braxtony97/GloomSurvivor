using Assets.GloomSurvivor.Scripts.Infrastructure.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataMonstersPath = "StaticData/Monsters";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticDataList";

        private Dictionary<Enums.MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<Enums.WindowId, WindowConfig> _windowConfigs;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(StaticDataMonstersPath)
                .ToDictionary(x => x.MonsterType, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .WindowConfigs
                .ToDictionary(x => x.WindowId, x => x); 
        }

        public MonsterStaticData GetMonster(Enums.MonsterTypeId monsterId) => 
            _monsters.TryGetValue(monsterId, out MonsterStaticData monsterData) 
                ? monsterData
                : null;

        public LevelStaticData GetLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData levelData)
                ? levelData
                : null;
          
        public WindowConfig GetWindow(Enums.WindowId windowId) => 
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
    }
}