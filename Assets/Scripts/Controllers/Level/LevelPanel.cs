using System;
using System.Collections.Generic;
using Controllers.Player;
using Interfaces;
using TMPro;
using UnityEngine;

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
        private bool gameStarted = false;
        [SerializeField] private TextMeshProUGUI coinCountText;
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
        }

        public void UpdateCoinCount(int count)
        {
            coinCountText.text = count.ToString();
        }

        public void OnRelentlessButtonClicked()
        {
            NotifyObservers(PlayerAction.RelentlessSkill);
            PlayerMovementController.Instance.RelentlessSkill();
        }

        public void OnInvulnerabilityButtonClicked()
        {
            NotifyObservers(PlayerAction.InvulnerabilitySkill);
            PlayerMovementController.Instance.InvulnerabilitySkill();
            
        }

        public void OnCrash()
        {
            NotifyObservers(PlayerAction.Crash);
        }

        public void OnCollect()
        {
            NotifyObservers(PlayerAction.Collect);
            coinCount++;
            UpdateCoinCount(coinCount);
        }
    }
}