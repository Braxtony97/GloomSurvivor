using System;
using GloomSurvivor.Scripts.Characters.MainPlayer;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using UnityEngine;

namespace GloomSurvivor.Scripts.UI
{
    public class ActorHpBarUI : MonoBehaviour
    {
        [SerializeField] private HpBarUI _hpBarUI;
        
        private IHealth _health;
        
        public void Construct(IHealth playerHealth)
        {
            _health = playerHealth;

            _health.HealthChanged += UpdateHpBarHUD;
        }
        
        private void Start()
        {
            /*IHealth health = GetComponent<IHealth>();
            
            if (_health != null)
                Construct(health);*/
        }

        private void UpdateHpBarHUD()
        {
            _hpBarUI.SetValue(_health.CurrentHealth, _health.MaxHealth);
        }

        private void OnDestroy() => 
            _health.HealthChanged -= UpdateHpBarHUD;
    }
}