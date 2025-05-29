using GloomSurvivor.Scripts.Infrastructure.AssetManagment;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject CreateHero(GameObject initPoint) => 
            _assetProvider.Instantiate(AssetPath.HeroPath, initPoint.transform.position);

        public void CreateHud() => 
            _assetProvider.Instantiate(AssetPath.HudPath);
    }
}