using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Services;

namespace Assets.GloomSurvivor.Scripts.UI.Services
{
    public interface IWindowService : IService
    {
        void Open (Enums.WindowId windowId);
    }
}