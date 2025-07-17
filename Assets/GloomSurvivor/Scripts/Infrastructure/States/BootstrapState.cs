using GloomSurvivor.Scripts.Data.SaveLoad;
using GloomSurvivor.Scripts.Infrastructure.AssetManagment;
using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Services;
using GloomSurvivor.Scripts.Services.Input;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string BOOT = "Boot";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;
            
            RegisterService();
        }

        public void Enter()
        {
            _sceneLoader.Load(BOOT, onLoaded: EnterLoadLevel);
        }

        private void RegisterService()
        {
            _serviceLocator.RegisterSingle<IInputService>(InputService());
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
            _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(_serviceLocator.ResolveSingle<IAssetProvider>()));
            _serviceLocator.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(_serviceLocator.ResolveSingle<IPersistentProgressService>(), _serviceLocator.ResolveSingle<IGameFactory>() ));

            RegisterStaticData();
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadMonsters();
            _serviceLocator.RegisterSingle(staticDataService);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        
    }
}