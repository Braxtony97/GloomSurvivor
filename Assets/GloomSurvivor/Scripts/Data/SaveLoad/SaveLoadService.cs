using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace GloomSurvivor.Scripts.Data.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS = "Progress";
        
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.PlayerProgress);
            }
            
            PlayerPrefs.SetString(PROGRESS, _progressService.PlayerProgress.ToJson()); 
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(PROGRESS)?.ToDeserialized<PlayerProgress>();
    }
}