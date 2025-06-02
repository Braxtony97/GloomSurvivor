using UnityEngine;

namespace GloomSurvivor.Scripts.Data.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS = "Progress";

        public void SaveProgress()
        {
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(PROGRESS)?.ToDeserialized<PlayerProgress>();
    }
}