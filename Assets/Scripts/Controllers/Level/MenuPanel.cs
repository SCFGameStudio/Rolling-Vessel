using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Level
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject optionsPanel;

        public void PlayButtonClicked()
        {
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

        public void BackButtonClicked()
        {
            menuPanel.SetActive(true);
            optionsPanel.SetActive(false);
            PlayerPrefs.Save();
        }
    }
}