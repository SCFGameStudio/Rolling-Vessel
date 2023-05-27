using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class SensitivitySlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private void Update()
        {
            PlayerPrefs.SetFloat("InputSensitivity", _slider.value);
        }
    }
    
}