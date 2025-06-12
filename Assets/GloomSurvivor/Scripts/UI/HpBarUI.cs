using UnityEngine;
using UnityEngine.UI;

namespace GloomSurvivor.Scripts.UI
{
    public class HpBarUI
    {
        [SerializeField] private Image _image;
        
        public void SetValue(float currentHP, float maxHP) =>
            _image.fillAmount = currentHP / maxHP;
    }
}