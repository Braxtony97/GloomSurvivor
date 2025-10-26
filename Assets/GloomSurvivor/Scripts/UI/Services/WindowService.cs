using Assets.GloomSurvivor.Scripts.UI.Services.Factory;
using GloomSurvivor.Scripts.Infrastructure;

namespace Assets.GloomSurvivor.Scripts.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open (Enums.WindowId windowId)
        {
            switch (windowId)
            {
                case Enums.WindowId.Unknown:
                    break;
                case Enums.WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
            }
        }
    }
}
