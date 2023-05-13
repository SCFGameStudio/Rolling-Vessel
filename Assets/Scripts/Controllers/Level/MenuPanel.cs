using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Level
{
    public class MenuPanel : MonoBehaviour
    {

        public void PlayButtonClicked()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void OptionsButtonClicked()
        {
        }

        public void CreditsButtonClicked()
        {
        }
    }
}