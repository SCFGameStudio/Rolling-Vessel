using System;
using System.Globalization;
using Controllers.Player;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.Level
{
    public class LevelPanel : MonoBehaviour
    {
        private static LevelPanel _instance;

        public static LevelPanel Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("LevelPanel is null");
                }

                return _instance;
            }
        }
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            if (PlayerPrefs.GetString("MusicSetting") == "On")
            {
                AudioManager.instance.Play("InGameMusic");
            }
        }

        public int ComboCount;
        
        private float _totalDistanceTraveled;
        private int _coinCount;
        private bool _gameStarted;
        private PlayerManager _playerManager;
        
        [SerializeField] private GameObject levelPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private TextMeshProUGUI failPanelText;
        [SerializeField] private Image ınvulnerabilityImage;
        [SerializeField] private Image relentlessImage;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Update()
        {
            if (!_gameStarted) return;
            var distanceFloatThisFrame = PlayerMovementController.Instance.GameSpeed * Time.deltaTime * 100;
            var distanceThisFrame = Mathf.RoundToInt(distanceFloatThisFrame);
            _totalDistanceTraveled += distanceThisFrame;
        }
        
        public void StartGame(bool state) 
        {
            _gameStarted = state;
        }

        private void OnGUI()
        {
            highScoreText.text = _totalDistanceTraveled.ToString(CultureInfo.InvariantCulture);

            if (PlayerPhysicsController.Instance.IsInvulnerabilityAvailable == false)
            {
                ınvulnerabilityImage.color = new Color(100, 100, 100, 0.4f);
            }

            if (PlayerPhysicsController.Instance.IsInvulnerabilityAvailable)
            {
                ınvulnerabilityImage.color = new Color(100, 100, 100, 1f);
            }

            if (ComboCount >= 10)
            {
                relentlessImage.color = new Color(100, 100, 100, 1f);
            }

            if (ComboCount < 10)
            {
                relentlessImage.color = new Color(100, 100, 100, 0.4f);
            }
            
        }

        public void RelentlessButtonClicked()
        {
            if (ComboCount >= 10)
            {
                PlayerMovementController.Instance.RelentlessSkill();
                ComboCount = 0;
            }
            else
            {
                Debug.Log("Relentless is not available");
            }
        }

        public void InvulnerabilityButtonClicked()
        {
            if (PlayerPhysicsController.Instance.IsInvulnerabilityAvailable)
            {
                PlayerMovementController.Instance.InvulnerabilitySkill();
            }
            else
            {
                Debug.Log("Wait for cooldown");
            }

        }

        public void Crash()
        {
            if (PlayerPrefs.GetString("MusicSetting") == "On")
            {
                AudioManager.instance.Stop("InGameMusic");
                AudioManager.instance.Play("GameOverMusic");
            }
            levelPanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(true);
            failPanelText.text = _totalDistanceTraveled.ToString(CultureInfo.InvariantCulture);
        }

        public void RestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (PlayerPrefs.GetString("MusicSetting") == "On")
            {
                AudioManager.instance.Stop("GameOverMusic");
            }
        }

        public void MainMenuButtonClicked()
        {
            PlayerPrefsManager.SavePlayerPrefsData();
            if (PlayerPrefs.GetString("MusicSetting") == "On")
            {
                AudioManager.instance.Stop("GameOverMusic");
            }
            SceneManager.LoadScene(0);
        }

        public void Collect()
        {
            _totalDistanceTraveled += 1000;
            _coinCount++;
        }

        public void Kill()
        {
            ComboCount++;
        }
    }
}