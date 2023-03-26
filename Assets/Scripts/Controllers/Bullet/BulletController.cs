using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private CD_Player data;

        public GameObject cannonPrefab;
        public int poolSize = 10;
        private float shotTimer = 0f;
        private List<GameObject> cannonPool;
        private int poolIndex = 0;
        private GameObject cannonParent;

        void Start()
        {
            cannonParent = new GameObject("Cannons");
            cannonPool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject cannon = Instantiate(cannonPrefab, cannonParent.transform);
                cannon.tag = "Cannon";
                cannon.SetActive(false);
                cannonPool.Add(cannon);
            }
        }
    
        void Update()
        {
            shotTimer += Time.deltaTime;
            if (shotTimer >= data.CannonReloadDuration)
            {
                shotTimer = 0f;
                
                GameObject cannon = cannonPool[poolIndex];
                poolIndex = (poolIndex + 1) % poolSize;
                
                cannon.transform.position = transform.position;
                cannon.SetActive(true);
                cannon.transform.forward = transform.forward;
            }
            foreach (GameObject cannon in cannonPool)
            {
                if (cannon.activeInHierarchy)
                {
                    cannon.transform.Translate(Vector3.forward * data.CannonSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, cannon.transform.position) > 50f)
                    {
                        cannon.SetActive(false);
                    }
                }
            }
        }
    }
    
}