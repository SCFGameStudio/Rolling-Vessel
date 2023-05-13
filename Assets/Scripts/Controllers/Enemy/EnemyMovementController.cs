using Data.ValueObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        private Vector3 _currentEnemyEulerAngles;
        private float _randomSidewaysMovement;
        private bool _isActivated = false;
        private float _activationDistance = 80f;
        
        [SerializeField] private Transform playerTransform;
        [SerializeField] private EnemyData data;
        [SerializeField] private Transform enemyPivotPoint;

        private void Start()
        {
            _randomSidewaysMovement = (Random.Range(0, 2) * 2) - 1;
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            
            if (!_isActivated && distanceToPlayer <= _activationDistance)
            {
                _isActivated = true;
            }
            if (_isActivated)
            {
                MoveEnemy();
            }
        }

        private void MoveEnemy()
        {
            _currentEnemyEulerAngles += new Vector3(0, 0,_randomSidewaysMovement) * Time.deltaTime * data.movementData.EnemySidewaysSpeed;
            enemyPivotPoint.localEulerAngles = _currentEnemyEulerAngles;
        }

        internal void GetMovementData(EnemyMovementData enemyMovementData)
        {
            data.movementData = enemyMovementData;
        }
    }
}