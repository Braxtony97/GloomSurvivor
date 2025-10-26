using Assets.GloomSurvivor.Scripts.Infrastructure.StaticData;
using GloomSurvivor.Scripts.Infrastructure;
using GloomSurvivor.Scripts.Infrastructure.AssetManagment;
using GloomSurvivor.Scripts.Infrastructure.StaticData;
using UnityEngine;

namespace Assets.GloomSurvivor.Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private IStaticDataService _staticData;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindow(Enums.WindowId.Shop);
            Object.Instantiate(config.Prefab, _uiRoot); 
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}