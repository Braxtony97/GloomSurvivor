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
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            
            PlayerPrefs.SetString(PROGRESS, _progressService.Progress.ToJson()); 
        }

        public Progress LoadProgress() => 
            PlayerPrefs.GetString(PROGRESS)?.ToDeserialized<Progress>();
    }
}