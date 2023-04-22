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
        private float _totalDistanceTraveled;
        private int _coinCount;
        public int comboCount;
        private bool _gameStarted;
        private PlayerManager _playerManager;
        [SerializeField]private GameObject levelPanel;
        [SerializeField]private GameObject losePanel;
        [SerializeField] private TextMeshProUGUI failPanelText;
        [SerializeField] private Image ınvulnerabilityImage;
        [SerializeField] private Image relentlessImage;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            if (!_gameStarted) return;
            var distanceFloatThisFrame = PlayerMovementController.Instance.gameSpeed * Time.deltaTime * 100;
            var distanceThisFrame = Mathf.RoundToInt(distanceFloatThisFrame);
            _totalDistanceTraveled += distanceThisFrame;
        }
        
        
        public void StartGame(bool state) {
            _gameStarted = state;
        }

        private void OnGUI()
        {
            highScoreText.text = _totalDistanceTraveled.ToString(CultureInfo.InvariantCulture);

            if (PlayerPhysicsController.Instance.isInvulnerabilityAvailable == false)
            {
                ınvulnerabilityImage.color = new Color(100, 100, 100, 0.4f);
            }

            if (PlayerPhysicsController.Instance.isInvulnerabilityAvailable)
            {
                ınvulnerabilityImage.color = new Color(100, 100, 100, 1f);
            }

            if (comboCount >= 10)
            {
                relentlessImage.color = new Color(100, 100, 100, 1f);
            }

            if (comboCount < 10)
            {
                relentlessImage.color = new Color(100, 100, 100, 0.4f);
            }
            
        }

        public void OnRelentlessButtonClicked()
        {
            if (comboCount >= 10)
            {
                PlayerMovementController.Instance.RelentlessSkill();
                comboCount = 0;
            }
            else
            {
                Debug.Log("Relentless is not available");
            }
        }

        public void OnInvulnerabilityButtonClicked()
        {
            if (PlayerPhysicsController.Instance.isInvulnerabilityAvailable)
            {
                PlayerMovementController.Instance.InvulnerabilitySkill();
            }
            else
            {
                Debug.Log("Wait for cooldown");
            }

        }

        public void OnCrash()
        {
            levelPanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(true);
            failPanelText.text = _totalDistanceTraveled.ToString(CultureInfo.InvariantCulture);
        }

        public void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene(0);
        }

        public void OnCollect()
        {
            _totalDistanceTraveled += 1000;
            _coinCount++;
        }

        public void OnKill()
        {
            comboCount++;
            Debug.Log(comboCount);
        }
    }
}