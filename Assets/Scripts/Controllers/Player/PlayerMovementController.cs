using System.Collections;
using Controllers.Bullet;
using Controllers.Level;
using Data.ValueObjects;
using Managers;
using Sirenix.OdinInspector;
using Unity.Mathematics;
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
        private Vector3 _currentEulerAngles;
        public Transform pivotPoint;
        private bool _hasBeenTriggered;
        private Vector3 _startPosition;
        public int gameSpeed = 1;
        public bool isRelentless;
        #endregion
        
        
        [SerializeField] private new Collider collider;
        [SerializeField] private PlayerData data;

        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;

        internal void GetMovementData(MovementData movementData)
        {
            data.MovementData = movementData;
        }
        
        internal void GetRelentlessData(RelentlessData relentlessData)
        {
            data.RelentlessData = relentlessData;
        }

        internal void GetInvulnerabilityData(InvulnerabilityData ınvulnerabilityData)
        {
            data.InvulnerabilityData = ınvulnerabilityData;
        }

        internal void GetCannonData(CannonData cannonData)
        {
            data.CannonData = cannonData;
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        public float GetDistanceTraveled()
        {
            return Vector3.Distance(_startPosition, transform.position);
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

            if (Input.GetMouseButtonDown(0) && !_hasBeenTriggered)
            {
                InvokeRepeating(nameof(SpeedIncrease), 10f, 10f);
                _hasBeenTriggered = true;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void MovePlayer()
        {
            pivotPoint.transform.Translate(Vector3.forward * data.MovementData.ForwardSpeed * Time.deltaTime);
            
            if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X");
                _currentEulerAngles += new Vector3(0, 0, mouseX) * Time.deltaTime * data.MovementData.SidewaysSpeed;
                _currentEulerAngles.z = Mathf.Clamp(_currentEulerAngles.z, -16f, 16f);
                pivotPoint.localEulerAngles = _currentEulerAngles;
            }
        }

        public void StopPlayer()
        {
            pivotPoint.transform.Translate(Vector3.forward * (0 * Time.deltaTime));
            pivotPoint.localEulerAngles = _currentEulerAngles;
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
            data.MovementData.ForwardSpeed += 3f;
            data.MovementData.SidewaysSpeed += 3f;
            data.CannonData.CannonSpeed += 3f;
            BulletController.Instance.GetCannonData(data.CannonData);
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
            collider.enabled = false;
            data.MovementData.ForwardSpeed += data.RelentlessData.RelentlessSpeed;
            data.CannonData.CannonSpeed += data.RelentlessData.RelentlessSpeed;
            BulletController.Instance.GetCannonData(data.CannonData);
            float sidewayspeed = data.MovementData.SidewaysSpeed;
            data.MovementData.SidewaysSpeed = 0;
            yield return new WaitForSeconds(3f);
            isRelentless = false;
            collider.enabled = true;
            data.MovementData.ForwardSpeed -= data.RelentlessData.RelentlessSpeed;
            data.CannonData.CannonSpeed -= data.RelentlessData.RelentlessSpeed;
            BulletController.Instance.GetCannonData(data.CannonData);
            data.MovementData.SidewaysSpeed += sidewayspeed;
        }
    }
}