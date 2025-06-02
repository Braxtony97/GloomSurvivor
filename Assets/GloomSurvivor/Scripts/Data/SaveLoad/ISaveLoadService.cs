using GloomSurvivor.Scripts.Services;

namespace GloomSurvivor.Scripts.Data.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}