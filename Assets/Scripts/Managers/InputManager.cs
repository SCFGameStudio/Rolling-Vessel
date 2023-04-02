using System;
using System.Collections.Generic;
using Controllers.Bullet;
using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects;
using Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using Keys;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [ShowInInspector] [Header("Data")] private InputData _data;
        [Space] [ShowInInspector] private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;

        private void Awake()
        {
            _data = GetInputData();
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }

        private void UnSubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && PlayerPhysicsController.Instance.ableToMove == true)
            {
                PlayerMovementController.Instance.IsReadyToMove(true);
                PlayerMovementController.Instance.IsReadyToPlay(true);
                BulletController.Instance.IsReadyToMove(true);
                BulletController.Instance.IsReadyToPlay(true);
            }
        }

        private void OnPlay()
        {
            _isAvailableForTouch = true;
        }

        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnReset()
        {
            PlayerMovementController.Instance.IsReadyToMove(false);
            PlayerMovementController.Instance.IsReadyToPlay(false);
        }
    }
}
