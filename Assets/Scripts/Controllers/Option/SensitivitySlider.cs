using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Option
{
    public class SensitivitySlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private void Awake()
        {
            PlayerPrefs.SetFloat("InputSensitivity", 1);
        }

        private void Start()
        {
            PlayerPrefs.GetFloat("InputSensitivity", _slider.value);
        }
        
        private void Update()
        {
            PlayerPrefs.SetFloat("InputSensitivity", _slider.value);
        }
    }
    
}