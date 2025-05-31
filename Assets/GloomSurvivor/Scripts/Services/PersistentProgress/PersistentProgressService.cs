using GloomSurvivor.Scripts.Data;

namespace GloomSurvivor.Scripts.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}