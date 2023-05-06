using System;

namespace Data.ValueObjects
{
    [Serializable]
    public struct EnemyData
    {
        public EnemyMovementData movementData;
    }
    
    [Serializable]
    public struct EnemyMovementData
    {
        public float EnemySidewaysSpeed;

        public EnemyMovementData(float enemySidewaysSpeed)
        {
            EnemySidewaysSpeed = enemySidewaysSpeed;
        }
    }
}
