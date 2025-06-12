using System;
using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace GloomSurvivor.Scripts.Characters.MainPlayer
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        public Action OnHealthChanged;
        public float CurrentHP
        {
            get => _playerData.CurrentHP;
            set
            {
                if (_playerData.CurrentHP != value)
                {
                    _playerData.CurrentHP = value;
                    OnHealthChanged?.Invoke();
                }
            }
        }

        public float MaxHP
        {
            get => _playerData.MaxHP; 
            set => _playerData.MaxHP = value;
        }


        private PlayerData _playerData;
        private PlayerAnimator _animator;


        public void LoadProgress(PlayerProgress playerProgress)
        {
            _playerData = playerProgress.PlayerData;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            _playerData.CurrentHP = CurrentHP;
            _playerData.MaxHP = MaxHP;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHP <= 0)
                return;

            _animator.PlayHit();
            CurrentHP -= damage;    
        }
    }
}