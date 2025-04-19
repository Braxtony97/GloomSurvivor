namespace Infrastructure
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
            Attack,
            Walking,
            Died,
            Running,
            Unknown
        }
    }
}