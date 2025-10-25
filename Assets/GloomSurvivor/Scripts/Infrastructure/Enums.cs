namespace GloomSurvivor.Scripts.Infrastructure
{
    public static class Enums
    {
        public enum Scenes
        {
            Boot,
            Main
        }
        
        public enum AnimatorState
        {
            Idle,
            AttackDefault,
            Walking,
            Died,
            Running,
            Unknown
        }

        public enum MonsterTypeId
        {
            Swordman = 0,
            Mage = 10
        }

        public enum WindowId
        {
            Unknown = 0,
            Shop = 1
        }
    }
}