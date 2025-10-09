    using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Data.SaveLoad;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Services.PersistentProgress;

namespace GloomSurvivor.Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            
            _gameStateMachine.Enter<LoadSceneState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private Progress NewProgress()
        {
            var playerProgress = new Progress("Main");

            playerProgress.PlayerData.MaxHP = 200f;
            playerProgress.PlayerData.ResetHP();

            playerProgress.PlayerStats.Damage = 20f;
            playerProgress.PlayerStats.DamageRadius = 1f;
            
            return playerProgress;
        }
    }
}