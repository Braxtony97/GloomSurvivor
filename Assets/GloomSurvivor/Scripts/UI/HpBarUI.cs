using UnityEngine;
using UnityEngine.UI;

namespace GloomSurvivor.Scripts.UI
{
    public class HpBarUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        public void SetValue(float currentHP, float maxHP)
        {
            _slider.value = currentHP / maxHP;
            
            Debug.Log(_slider.value);
        }
    }
}