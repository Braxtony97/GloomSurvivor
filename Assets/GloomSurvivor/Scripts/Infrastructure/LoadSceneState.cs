using Infrastructure;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public class LoadSceneState : IState
    {
        private const string Main = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(Main);  
        }

        public void Exit()
        {
        }
    }
}