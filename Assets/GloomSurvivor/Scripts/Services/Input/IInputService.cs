using UnityEngine;

namespace GloomSurvivor.Scripts.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButton();
    }
}