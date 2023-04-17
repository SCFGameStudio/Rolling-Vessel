using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Enemy", menuName = "RollingVessel/CD_Enemy", order = 0)]
    public class CD_Enemy : ScriptableObject
    {
        public EnemyData Data;
    }
}