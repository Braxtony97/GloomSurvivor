using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using Services.Input;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
        public static IInputService InputService;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}