using Assets.GloomSurvivor.Scripts.UI.Services;
using GloomSurvivor.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GloomSurvivor.Scripts.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public Enums.WindowId WindowId;
        private IWindowService _windowServices;

        public void Construct(IWindowService windowServices) => 
            _windowServices = windowServices;

        private void Awake()
        {
            Button.onClick.AddListener(Open);
        }

        private void Open() => 
            _windowServices.Open(WindowId);
    }
}