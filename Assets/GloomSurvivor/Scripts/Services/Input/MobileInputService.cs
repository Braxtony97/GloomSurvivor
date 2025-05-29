using UnityEngine;

namespace GloomSurvivor.Scripts.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => MobileAxis();
    }
}