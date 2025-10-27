using TMPro;
using UnityEngine;

namespace Assets.GloomSurvivor.Scripts.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TMP_Text _countGold;

        protected override void Initialize()
        {
            base.Initialize();
            RefreshGoldCount();
        }

        protected override void SubscribeUpdates() => 
            Progress.WorldData.LootData.Changed += RefreshGoldCount;

        private void RefreshGoldCount() => 
            _countGold.text = Progress.WorldData.LootData.Collected.ToString();

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            Progress.WorldData.LootData.Changed -= RefreshGoldCount;
        }
    }
}
