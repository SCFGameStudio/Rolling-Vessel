using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Level;
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
        private bool hasBeenTriggered = false;
        private Vector3 startPosition;
        public int gameSpeed = 1;
        public bool isRelentless;
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

        private void Start()
        {
            startPosition = transform.position;
        }

        public float GetDistanceTraveled()
        {
            return Vector3.Distance(startPosition, transform.position);
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
                LevelPanel.Instance.StartGame(true);
            }

            if (Input.GetMouseButtonDown(0) && !hasBeenTriggered)
            {
                InvokeRepeating("SpeedIncrease", 10f, 10f);
                hasBeenTriggered = true;
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
            Debug.Log("SpeedIncreased");
            gameSpeed++;
            _movementData.ForwardSpeed += 3f;
            _movementData.SidewaysSpeed += 3f;
            _cannonData.CannonSpeed += 3f;
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
            isRelentless = true;
            RotationManager.Instance.DisableAllTurnLevelScripts(transform.root);
            collider.enabled = false;
            _movementData.ForwardSpeed += _relentlessData.RelentlessSpeed;
            float sidewayspeed = _movementData.SidewaysSpeed;
            _movementData.SidewaysSpeed = 0;
            _cannonData.CannonSpeed += _relentlessData.RelentlessSpeed;
            yield return new WaitForSeconds(3f);
            isRelentless = false;
            RotationManager.Instance.EnableAllTurnLevelScripts(transform.root);
            collider.enabled = true;
            _movementData.ForwardSpeed -= _relentlessData.RelentlessSpeed;
            _movementData.SidewaysSpeed += sidewayspeed;
            _cannonData.CannonSpeed -= _relentlessData.RelentlessSpeed;
        }
    }
}