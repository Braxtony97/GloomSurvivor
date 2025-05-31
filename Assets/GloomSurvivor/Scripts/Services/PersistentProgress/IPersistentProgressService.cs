using GloomSurvivor.Scripts.Data;

namespace GloomSurvivor.Scripts.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}