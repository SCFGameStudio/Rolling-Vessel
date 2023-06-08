using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Option
{
    public class OptionPanel : MonoBehaviour
    {


        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject optionsPanel;
        [SerializeField] private Image musicSprite;
        [SerializeField] private Image soundSprite;
        [SerializeField] private Sprite musicOn;
        [SerializeField] private Sprite musicOff;
        [SerializeField] private Sprite soundOn;
        [SerializeField] private Sprite soundOff;

        private void Awake()
        {
            PlayerPrefs.SetString("MusicSetting", "On");
            PlayerPrefs.SetString("SoundSetting", "On");
        }


        public void MusicButtonClicked()
        {
            if (PlayerPrefs.GetString("MusicSetting") == "On")
            {
                PlayerPrefs.SetString("MusicSetting", "Off");
                musicSprite.sprite = musicOff;
            }
            else
            {
                PlayerPrefs.SetString("MusicSetting", "On");
                musicSprite.sprite = musicOn;
            }

        }

        public void SoundButtonClicked()
        {
            if (PlayerPrefs.GetString("SoundSetting") == "On")
            {
                PlayerPrefs.SetString("SoundSetting", "Off");
                soundSprite.sprite = soundOff;
            }
            
            else
            {
                PlayerPrefs.SetString("SoundSetting", "On");
                soundSprite.sprite = soundOn;
            }
        }
        
        public void BackButtonClicked()
        {
            menuPanel.SetActive(true);
            optionsPanel.SetActive(false);
            PlayerPrefsManager.SavePlayerPrefsData();
        }
        
    }
}