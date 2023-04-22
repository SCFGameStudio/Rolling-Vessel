using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Level
{
    public class MenuPanel : MonoBehaviour
    {

        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void OnOptionsButtonClicked()
        {
        }

        public void OnCreditsButtonClicked()
        {
        }
    }
}