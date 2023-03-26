using UnityEngine;
using Data.ValueObjects;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "RollingVessel/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        //public PlayerData Data;
        
        public float ForwardSpeed;
        public float SidewaysSpeed;
        public float CannonSpeed;
        public float CannonReloadDuration;
        public float RelentlessSpeed;
        public float InvulnerabilityDuration;
    }
}