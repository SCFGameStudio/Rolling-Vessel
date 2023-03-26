using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using Sirenix.OdinInspector;
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

        

        
        private Vector3 currentEulerAngles;
        public Transform pivotPoint;
        #endregion
        
        
        [SerializeField] private new Collider collider;
        //[SerializeField] private PlayerManager manager;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private CD_Player data;

        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;


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
            pivotPoint.transform.Translate(Vector3.forward * data.ForwardSpeed * Time.deltaTime);
            //Test için tuşlar atandı. İleride mouse sürüklenerek çalışması yapılacak.

            if (Input.GetMouseButton(0))
            {
                currentEulerAngles += new Vector3(0, 0, -1) * Time.deltaTime * data.SidewaysSpeed;
                currentEulerAngles.z = Mathf.Clamp(currentEulerAngles.z, -16f, 16f);
                pivotPoint.localEulerAngles = currentEulerAngles;

            }

            if (Input.GetMouseButton(1))
            {
                currentEulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * data.SidewaysSpeed;
                currentEulerAngles.z = Mathf.Clamp(currentEulerAngles.z, -16f, 16f);
                pivotPoint.localEulerAngles = currentEulerAngles;
            }
            //Test için tuş atandı. İleride butona basarak etkinleşecek.

            if (Input.GetKeyUp(KeyCode.E))
            {
                InvulnerabilitySkill();
            }
            //Test için tuş atandı. İleride komboya göre etkinleşecek.

            if (Input.GetKeyUp(KeyCode.Space))
            {
                RelentlessSkill();
            }
        }

        public void StopPlayer()
        {
            pivotPoint.transform.Translate(Vector3.forward * 0 * Time.deltaTime);
            pivotPoint.localEulerAngles = currentEulerAngles;
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }

        private void StopPlayerHorizontaly()
        {
            
        }

        private void RelentlessSkill()
        {
            StartCoroutine(Relentless());
            Debug.Log("You are relentless");
        }

        private void InvulnerabilitySkill()
        {
            Debug.Log("Dodging");
            StartCoroutine(PlayerPhysicsController.Instance.Invulnerability());
        }

        private void SpeedIncrease()
        {
            data.ForwardSpeed += data.ForwardSpeed * 5;
            data.SidewaysSpeed += data.SidewaysSpeed * 5;
        }

        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }
        
        //UpdateInputParams


        internal void OnReset()
        {
            StopPlayer();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }

        IEnumerator Relentless()
        {
            TurnLevelScript.Instance.enabled = false;
            collider.enabled = false;
            data.ForwardSpeed += data.RelentlessSpeed;
            float sidewayspeed = data.SidewaysSpeed;
            data.SidewaysSpeed = 0;
            data.CannonSpeed += data.RelentlessSpeed;
            yield return new WaitForSeconds(3f);
            TurnLevelScript.Instance.enabled = true;
            collider.enabled = true;
            data.ForwardSpeed -= data.RelentlessSpeed;
            data.SidewaysSpeed += sidewayspeed;
            data.CannonSpeed -= data.RelentlessSpeed;

        }
    }
}