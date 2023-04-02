using System;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.Bullet
{
    public class BulletController : MonoBehaviour
    {
        private static BulletController _instance;

        public static BulletController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("BulletController is null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        [SerializeField] private CannonData _cannonData;

        public GameObject cannonPrefab;
        public int poolSize = 10;
        private float shotTimer = 0f;
        private List<GameObject> cannonPool;
        private int poolIndex = 0;
        private GameObject cannonParent;
        [SerializeField] private PlayerManager manager;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        
        internal void GetCannonData(CannonData cannonData)
        {
            _cannonData = cannonData;
        }

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
        
        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }
        
    
        void Update()
        {
            if (_isReadyToMove && _isReadyToPlay)
            {
                shotTimer += Time.deltaTime;
                if (shotTimer >= _cannonData.CannonReloadDuration)
                {
                    shotTimer = 0f;

                    GameObject cannon = cannonPool[poolIndex];
                    poolIndex = (poolIndex + 1) % poolSize;

                    cannon.transform.position = transform.position + new Vector3(0,1,0);
                    cannon.SetActive(true);
                    cannon.transform.forward = transform.forward;
                }

                foreach (GameObject cannon in cannonPool)
                {
                    if (cannon.activeInHierarchy)
                    {
                        cannon.transform.Translate(Vector3.forward * _cannonData.CannonSpeed * Time.deltaTime);
                        if (Vector3.Distance(transform.position, cannon.transform.position) > 50f)
                        {
                            cannon.SetActive(false);
                        }
                    }
                }
            }
        }
    }
    
}