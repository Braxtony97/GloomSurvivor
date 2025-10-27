using Assets.GloomSurvivor.Scripts.Infrastructure.StaticData;
using Assets.GloomSurvivor.Scripts.UI.Windows;
using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Infrastructure.AssetManagment;
using GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace Assets.GloomSurvivor.Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private IStaticDataService _staticData;
        private IPersistentProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindow(Enums.WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}