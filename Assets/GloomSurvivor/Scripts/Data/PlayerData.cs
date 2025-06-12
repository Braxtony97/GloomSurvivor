using System;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable]
    public class PlayerData
    {
        public float CurrentHP;
        public float MaxHP;
        
        public void ResetHP() => 
            CurrentHP = MaxHP;
    }
}