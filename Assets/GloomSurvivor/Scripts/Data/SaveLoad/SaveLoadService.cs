using GloomSurvivor.Scripts.Infrastructure.Factory;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using System.IO;
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

            string filePath = Path.Combine(Application.persistentDataPath, "save.json");
            string json = _progressService.Progress.ToJson();
            File.WriteAllText(filePath, json);

            Debug.Log($"Progress saved to: {filePath}");
        }

        public Progress LoadProgress() => 
            PlayerPrefs.GetString(PROGRESS)?.ToDeserialized<Progress>();
    }
}