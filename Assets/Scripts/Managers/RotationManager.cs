using System.Collections.Generic;
using Controllers.Level;
using UnityEngine;

namespace Managers
{
    public class RotationManager : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 185f;
        [SerializeField] private float searchInterval = 1f; 
        private readonly List<GameObject> _objectsToRotate = new();

        private static RotationManager _instance;
        public static RotationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("RotationManagerScript is null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            StartCoroutine(SearchForNewObjects());
        }
        
        private IEnumerator<WaitForSeconds> SearchForNewObjects()
        {
            while (true)
            {
                var newObjects = GameObject.FindGameObjectsWithTag("Level");
                
                foreach (GameObject obj in newObjects)
                {
                    if (_objectsToRotate.Contains(obj)) continue;
                    obj.AddComponent<TurnLevelScript>();
                    _objectsToRotate.Add(obj);
                }
                
                yield return new WaitForSeconds(searchInterval);
            }
        }

        public float GetRotationSpeed()
        {
            return rotationSpeed;
        }
    }
    
}