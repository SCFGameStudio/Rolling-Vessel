using UnityEngine;

namespace Managers
{
    public class PlayerPrefsManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        public static void SavePlayerPrefsData()
        {
            PlayerPrefs.Save();
        }
    }
}