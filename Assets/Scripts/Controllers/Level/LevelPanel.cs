using System;
using System.Collections.Generic;
using Controllers.Player;
using Interfaces;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.Level
{
    public class LevelPanel : MonoBehaviour, ILevelPanelSubject
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
        
        private List<ILevelPanelObserver> observers = new List<ILevelPanelObserver>();
        private float totalDistanceTraveled = 0;
        private int coinCount = 0;
        public int comboCount = 0;
        private bool gameStarted = false;
        private PlayerManager playerManager;
        [SerializeField]private GameObject levelPanel;
        [SerializeField]private GameObject losePanel;
        [SerializeField] private TextMeshProUGUI failPanelText;

        [SerializeField] private Image ınvulnerabilityImage;
        [SerializeField] private Image relentlessImage;
        //[SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Awake()
        {
            _instance = this;
        }

        public void AddObserver(ILevelPanelObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(ILevelPanelObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(PlayerAction action)
        {
            foreach (ILevelPanelObserver observer in observers)
            {
                observer.OnPlayerAction(action);
            }
        }

        private void Update()
        {
            if (gameStarted == true)
            {
                float distanceFloatThisFrame = PlayerMovementController.Instance.gameSpeed * Time.deltaTime * 100;
                int distanceThisFrame = Mathf.RoundToInt(distanceFloatThisFrame);
                totalDistanceTraveled += distanceThisFrame;
            }
        }
        
        
        public void StartGame(bool state) {
            gameStarted = state;
        }

        private void OnGUI()
        {
            highScoreText.text = totalDistanceTraveled.ToString();

            if (PlayerPhysicsController.Instance.isInvulnerabilityAvailable == false)
            {
                ınvulnerabilityImage.color = new Color(100, 100, 100, 0.4f);
            }

            if (PlayerPhysicsController.Instance.isInvulnerabilityAvailable == true)
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

        //public void UpdateCoinCount(int count)
        //{
        //    coinCountText.text = count.ToString();
        //}

        public void OnRelentlessButtonClicked()
        {
            if (comboCount >= 10)
            {
                NotifyObservers(PlayerAction.RelentlessSkill);
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
            if (PlayerPhysicsController.Instance.isInvulnerabilityAvailable == true)
            {
                NotifyObservers(PlayerAction.InvulnerabilitySkill);
                PlayerMovementController.Instance.InvulnerabilitySkill();
            }
            else
            {
                Debug.Log("Wait for cooldown");
            }

        }

        public void OnCrash()
        {
            NotifyObservers(PlayerAction.Crash);
            levelPanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(true);
            failPanelText.text = totalDistanceTraveled.ToString();
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
            NotifyObservers(PlayerAction.Collect);
            totalDistanceTraveled += 1000;
            coinCount++;
            //UpdateCoinCount(coinCount);
        }

        public void OnKill()
        {
            NotifyObservers(PlayerAction.Kill);
            comboCount++;
            Debug.Log(comboCount);
        }
    }
}