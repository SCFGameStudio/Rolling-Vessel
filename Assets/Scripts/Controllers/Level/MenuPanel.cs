using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.Level
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject optionsPanel;

        public void PlayButtonClicked()
        {
            PlayerPrefsManager.SavePlayerPrefsData();
            SceneManager.LoadScene("MainScene");
        }

        public void OptionsButtonClicked()
        {
            menuPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }

        public void CreditsButtonClicked()
        {
        }
    }
}