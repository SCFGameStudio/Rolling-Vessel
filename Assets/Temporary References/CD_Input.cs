using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "Rolling Vessel/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData data;
    }
}