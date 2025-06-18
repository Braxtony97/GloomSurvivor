using System;

namespace GloomSurvivor.Scripts.Infrastructure.Interfaces
{
    public interface IHealth
    {
        event Action HealthChanged;
        float MaxHealth { get; set; }
        float CurrentHealth { get; set; }
        void TakeDamage(float damage);
    }
}