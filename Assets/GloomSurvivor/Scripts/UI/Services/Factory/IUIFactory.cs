using GloomSurvivor.Scripts.Services;

namespace Assets.GloomSurvivor.Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreateUIRoot();
    }
}