using System;
using Controllers.Enemy;
using Data.UnityObjects;
using Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyMovementController enemyMovementController;
        [SerializeField] private EnemyPhysicsController enemyPhysicsController;

        [ShowInInspector] private EnemyData _data;

        private void Awake()
        {
            _data = GetEnemyData();
            SendDataToControllers();
        }

        private static EnemyData GetEnemyData()
        {
            return Resources.Load<CD_Enemy>("Data/CD_Enemy").Data;
        }

        private void SendDataToControllers()
        {
            enemyMovementController.GetMovementData(_data.movementData);
        }
    }
}