using System;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable ]
    public class PlayerProgress
    {
        public PlayerData PlayerData; 
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            PlayerData = new PlayerData();
            WorldData = new WorldData(initialLevel);
        }
    }
}