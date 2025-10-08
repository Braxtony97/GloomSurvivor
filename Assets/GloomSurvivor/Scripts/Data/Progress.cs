using System;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable ]
    public class Progress
    {
        public PlayerData PlayerData; 
        public WorldData WorldData;
        public PlayerStats PlayerStats;
        public KillData KillData;

        public Progress(string initialLevel)
        {
            PlayerData = new PlayerData();
            WorldData = new WorldData(initialLevel);
            PlayerStats = new PlayerStats();
            KillData = new KillData();
        }
    }
}