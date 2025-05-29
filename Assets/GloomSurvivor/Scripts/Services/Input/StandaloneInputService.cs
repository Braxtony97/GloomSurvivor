using UnityEngine;

namespace GloomSurvivor.Scripts.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = MobileAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();

                return axis;
            }
        }

        private Vector2 UnityAxis() =>
            new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}