using System;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;
        public WorldLootData WorldLootData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            LootData = new LootData();
            WorldLootData = new WorldLootData();
        }
    }
}