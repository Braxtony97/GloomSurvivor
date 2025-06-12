using GloomSurvivor.Scripts.Characters.MainPlayer;
using UnityEngine;

namespace GloomSurvivor.Scripts.UI
{
    public class ActorHpBarUI
    {
        [SerializeField] private HpBarUI _hpBarUI;
        private PlayerHealth _playerHealth;
        
        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;

            _playerHealth.OnHealthChanged += UpdateHpBarHUD;
        }

        private void UpdateHpBarHUD()
        {
            _hpBarUI.SetValue(_playerHealth.CurrentHP, _playerHealth.MaxHP);
        }

        private void OnDestroy() => 
            _playerHealth.OnHealthChanged -= UpdateHpBarHUD;
    }
}