using UnityEngine;

namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Fire = "Fire";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButton() => 
            SimpleInput.GetButtonUp(Fire);
        
        protected Vector2 MobileAxis() => 
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}