using System;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable ]
    public class PlayerProgress
    {
        public PlayerData PlayerData; 
        public WorldData WorldData;
        public PlayerStats PlayerStats;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            PlayerData = new PlayerData();
            WorldData = new WorldData(initialLevel);
            PlayerStats = new PlayerStats();
            KillData = new KillData();
        }
    }
}