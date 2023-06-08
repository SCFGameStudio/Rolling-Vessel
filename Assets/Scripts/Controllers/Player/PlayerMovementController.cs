using System.Collections;
using Controllers.Bullet;
using Controllers.Level;
using Data.ValueObjects;
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
        
        private void Awake()
        {
            _instance = this;
            inputSensitivity = PlayerPrefs.GetFloat("InputSensitivity");
        }
        
        [ShowInInspector] private float _xValue;
        
        private float2 _clampValues;
        private Vector3 _currentEulerAngles;
        private bool _hasBeenTriggered;
        private Vector3 _startPosition;
        private float inputSensitivity;
        
        public int GameSpeed = 1;
        public bool IsRelentless;

        [SerializeField] private Transform pivotPoint;
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
            Debug.Log(inputSensitivity);
        }

        public float GetDistanceTraveled()
        {
            return Vector3.Distance(_startPosition, transform.position);
        }

        private void Update()
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
                PlayerMeshController.Instance.PlayWaterParticle();
                InvokeRepeating(nameof(SpeedIncrease), 10f, 10f);
                _hasBeenTriggered = true;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void MovePlayer()
        {
            pivotPoint.transform.Translate(Vector3.forward * data.MovementData.ForwardSpeed * Time.deltaTime);
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Moved) return;
            var touchDeltaX = touch.deltaPosition.x;
            _currentEulerAngles += new Vector3(0, 0, touchDeltaX) * Time.deltaTime * data.MovementData.SidewaysSpeed * inputSensitivity;
            _currentEulerAngles.z = Mathf.Clamp(_currentEulerAngles.z, -16f, 16f);
            pivotPoint.localEulerAngles = _currentEulerAngles;
        }

        public void StopPlayer()
        {
            pivotPoint.transform.Translate(Vector3.forward * (0 * Time.deltaTime));
            pivotPoint.localEulerAngles = _currentEulerAngles;
        }

        public void RelentlessSkill()
        {
            StartCoroutine(Relentless());
        }

        public void InvulnerabilitySkill()
        {
            StartCoroutine(PlayerPhysicsController.Instance.Invulnerability());
        }

        private void SpeedIncrease()
        {
            GameSpeed++;
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

        IEnumerator Relentless()
        {
            IsRelentless = true;
            collider.enabled = false;
            data.MovementData.ForwardSpeed += data.RelentlessData.RelentlessSpeed;
            data.CannonData.CannonSpeed += data.RelentlessData.RelentlessSpeed;
            BulletController.Instance.GetCannonData(data.CannonData);
            float sidewaysSpeed = data.MovementData.SidewaysSpeed;
            data.MovementData.SidewaysSpeed = 0;
            yield return new WaitForSeconds(3f);
            IsRelentless = false;
            collider.enabled = true;
            data.MovementData.ForwardSpeed -= data.RelentlessData.RelentlessSpeed;
            data.CannonData.CannonSpeed -= data.RelentlessData.RelentlessSpeed;
            BulletController.Instance.GetCannonData(data.CannonData);
            data.MovementData.SidewaysSpeed += sidewaysSpeed;
        }
    }
}