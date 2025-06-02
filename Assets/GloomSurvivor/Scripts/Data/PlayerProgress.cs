using System;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable ]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
        }
    }
}