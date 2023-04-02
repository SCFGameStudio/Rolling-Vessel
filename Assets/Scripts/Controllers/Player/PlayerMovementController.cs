using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using Keys;
using Managers;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Instance

        

        
        private static PlayerMovementController _instance;

        public static PlayerMovementController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("PlayerMovementController is null");
                }

                return _instance;
            }
        }
        #endregion

        #region Private variables


        [ShowInInspector] private float _xValue;
        private float2 _clampValues;
        private Vector3 currentEulerAngles;
        public Transform pivotPoint;
        #endregion
        
        
        [SerializeField] private new Collider collider;
        [SerializeField] private PlayerManager manager;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerData data;
        [ShowInInspector] private MovementData _movementData;
        [ShowInInspector] private RelentlessData _relentlessData;
        [ShowInInspector] private InvulnerabilityData _ınvulnerabilityData;
        [ShowInInspector] private CannonData _cannonData;

        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;

        internal void GetMovementData(MovementData movementData)
        {
            _movementData = movementData;
        }
        
        internal void GetRelentlessData(RelentlessData relentlessData)
        {
            _relentlessData = relentlessData;
        }

        internal void GetInvulnerabilityData(InvulnerabilityData ınvulnerabilityData)
        {
            _ınvulnerabilityData = ınvulnerabilityData;
        }

        internal void GetCannonData(CannonData cannonData)
        {
            _cannonData = cannonData;
        }
        
        


        private void Awake()
        {
            _instance = this;
        }

        private void LateUpdate()
        {
            if (!_isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (_isReadyToMove)
            {
                MovePlayer();
            }
            else StopPlayerHorizontaly();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void MovePlayer()
        {
            pivotPoint.transform.Translate(Vector3.forward * _movementData.ForwardSpeed * Time.deltaTime);
            
            if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X");
                currentEulerAngles += new Vector3(0, 0, mouseX) * Time.deltaTime * _movementData.SidewaysSpeed;
                currentEulerAngles.z = Mathf.Clamp(currentEulerAngles.z, -16f, 16f);
                pivotPoint.localEulerAngles = currentEulerAngles;
            }
        }

        public void StopPlayer()
        {
            pivotPoint.transform.Translate(Vector3.forward * 0 * Time.deltaTime);
            pivotPoint.localEulerAngles = currentEulerAngles;
        }

        private void StopPlayerHorizontaly()
        {
            
        }

        public void RelentlessSkill()
        {
            StartCoroutine(Relentless());
            Debug.Log("You are relentless");
        }

        public void InvulnerabilitySkill()
        {
            Debug.Log("Dodging");
            StartCoroutine(PlayerPhysicsController.Instance.Invulnerability());
        }

        private void SpeedIncrease()
        {
            _movementData.ForwardSpeed += _movementData.ForwardSpeed + 5;
            _movementData.SidewaysSpeed += _movementData.SidewaysSpeed + 5;
        }

        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }


        internal void OnReset()
        {
            StopPlayer();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }

        IEnumerator Relentless()
        {
            TurnLevelScript[] turnLevelScripts = FindObjectsOfType<TurnLevelScript>();
            foreach (TurnLevelScript tls in turnLevelScripts) {
                tls.enabled = false;
            }
            collider.enabled = false;
            _movementData.ForwardSpeed += _relentlessData.RelentlessSpeed;
            float sidewayspeed = _movementData.SidewaysSpeed;
            _movementData.SidewaysSpeed = 0;
            _cannonData.CannonSpeed += _relentlessData.RelentlessSpeed;
            yield return new WaitForSeconds(3f);
            FindObjectsOfType<TurnLevelScript>();
            foreach (TurnLevelScript tls in turnLevelScripts) {
                tls.enabled = true;
            }
            collider.enabled = true;
            _movementData.ForwardSpeed -= _relentlessData.RelentlessSpeed;
            _movementData.SidewaysSpeed += sidewayspeed;
            _cannonData.CannonSpeed -= _relentlessData.RelentlessSpeed;

        }
    }
}