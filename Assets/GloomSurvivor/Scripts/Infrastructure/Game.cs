using GloomSurvivor.Scripts.Infrastructure;
using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
        public static IInputService InputService;

        public Game()
        {
            StateMachine = new GameStateMachine();
        }
    }
}