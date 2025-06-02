using System;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public Vector3Data Position;
        public string Level;

        public PositionOnLevel(Vector3Data position, string level)
        {
            Position = position;
            Level = level;
        }
    }
}