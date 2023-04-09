using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class RotationManager : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 185f;
        [SerializeField] private float searchInterval = 1f; 
        public List<GameObject> objectsToRotate = new List<GameObject>();

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
        }

        private void Start()
        {
            StartCoroutine(SearchForNewObjects());
        }

        private IEnumerator<WaitForSeconds> SearchForNewObjects()
        {
            while (true)
            {
                GameObject[] newObjects = GameObject.FindGameObjectsWithTag("Level");
                
                foreach (GameObject obj in newObjects)
                {
                    if (!objectsToRotate.Contains(obj))
                    {
                        obj.AddComponent<TurnLevelScript>();
                        objectsToRotate.Add(obj);
                    }
                }
                
                yield return new WaitForSeconds(searchInterval);
            }
        }
        
        public void RemoveObjectToRotate(GameObject obj)
        {
            if (objectsToRotate.Contains(obj))
            {
                obj.GetComponent<TurnLevelScript>().enabled = false;
                objectsToRotate.Remove(obj);
            }
        }

        private List<GameObject> GetObjectsToRotate()
        {
            
            GameObject[] levelObjects = GameObject.FindGameObjectsWithTag("Level");
            List<GameObject> objectsToRotate = new List<GameObject>();

            foreach (GameObject obj in levelObjects)
            {
                TurnLevelScript script = obj.GetComponent<TurnLevelScript>();

                if (script != null)
                {
                    objectsToRotate.Add(obj);
                }
            }

            return objectsToRotate;
        }
        
        public void DisableAllTurnLevelScripts(Transform root)
        {
            List<GameObject> objectsToDisable = GetObjectsToRotate();

            foreach (GameObject obj in objectsToDisable)
            {
                TurnLevelScript script = obj.GetComponent<TurnLevelScript>();

                if (script != null)
                {
                    script.enabled = false;
                }
            }

            foreach (Transform child in root)
            {
                DisableAllTurnLevelScripts(child);
            }
        }

        public void EnableAllTurnLevelScripts(Transform root)
        {
            List<GameObject> objectsToEnable = GetObjectsToRotate();

            foreach (GameObject obj in objectsToEnable)
            {
                TurnLevelScript script = obj.GetComponent<TurnLevelScript>();

                if (script != null)
                {
                    script.enabled = true;
                }
            }
            
            foreach (Transform child in root)
            {
                EnableAllTurnLevelScripts(child);
            }
        }

        public float GetRotationSpeed()
        {
            return rotationSpeed;
        }
    }
    
}