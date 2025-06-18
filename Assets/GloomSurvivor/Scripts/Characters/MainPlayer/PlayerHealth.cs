using System;
using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Infrastructure.Interfaces;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public event Action HealthChanged;
        public float CurrentHealth
        {
            get => _playerData.CurrentHP;
            set
            {
                if (_playerData.CurrentHP != value)
                {
                    _playerData.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float MaxHealth
        {
            get => _playerData.MaxHP; 
            set => _playerData.MaxHP = value;
        }


        [SerializeField] private PlayerAnimator _animator;
        private PlayerData _playerData;


        public void LoadProgress(PlayerProgress playerProgress)
        {
            _playerData = playerProgress.PlayerData;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            _playerData.CurrentHP = CurrentHealth;
            _playerData.MaxHP = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0)
                return;

            _animator.PlayHit();
            CurrentHealth -= damage;    
        }
    }
}