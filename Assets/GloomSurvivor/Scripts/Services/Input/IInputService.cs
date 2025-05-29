using UnityEngine;

namespace GloomSurvivor.Scripts.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButton();
    }
}