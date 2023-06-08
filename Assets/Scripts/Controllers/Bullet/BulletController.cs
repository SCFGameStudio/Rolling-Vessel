using System;
using System.Collections.Generic;
using Controllers.Level;
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

        [SerializeField] private PlayerData data;
        [SerializeField] private GameObject cannonPrefab;
        
        private int _poolSize = 10;
        private float _shotTimer;
        private List<GameObject> _cannonPool;
        private int _poolIndex;
        private GameObject _cannonParent;
        
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        
        internal void GetCannonData(CannonData cannonData)
        {
            data.CannonData = cannonData;
        }

        private void Start()
        {
            InstantiatePool();
        }

        private void InstantiatePool()
        {
            _cannonParent = new GameObject("Cannons");
            _cannonPool = new List<GameObject>();
            for (var i = 0; i < _poolSize; i++)
            {
                var cannon = Instantiate(cannonPrefab, _cannonParent.transform);
                cannon.tag = "Cannon";
                cannon.SetActive(false);
                _cannonPool.Add(cannon);
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
        
        private void DestroyObject(GameObject destroyGameObject)
        {
            Destroy(destroyGameObject);
        }


        private void FixedUpdate()
        {
            if (!_isReadyToMove || !_isReadyToPlay) return;
            CannonReload();
            
            foreach (var cannon in _cannonPool)
            {
                if (!cannon.activeInHierarchy) continue;
                cannon.transform.Translate(Vector3.forward * (data.CannonData.CannonSpeed * Time.deltaTime));
                if (Vector3.Distance(transform.position, cannon.transform.position) > 75f)
                {
                    cannon.SetActive(false);
                }
                        
                var colliders = Physics.OverlapSphere(cannon.transform.position, 0.2f);
                foreach (Collider collider1 in colliders)
                {
                    if (collider1.CompareTag("Obstacle"))
                    {
                        cannon.SetActive(false);
                        break;
                    }

                    if (collider1.CompareTag("Enemy"))
                    {
                        LevelPanel.Instance.Kill();
                        if (PlayerPrefs.GetString("SoundSetting") == "On")
                        {
                            AudioManager.instance.Play("EnemyCrashSound");
                        }

                        DestroyObject(collider1.gameObject);
                        cannon.SetActive(false);
                        break;

                    }
                }
            }
        }

        private void CannonReload()
        {
            _shotTimer += Time.deltaTime;
            if (_shotTimer >= data.CannonData.CannonReloadDuration)
            {
                _shotTimer = 0f;

                var cannon = _cannonPool[_poolIndex];
                _poolIndex = (_poolIndex + 1) % _poolSize;

                cannon.transform.position = transform.position + new Vector3(0, 1, 0);
                cannon.SetActive(true);
                cannon.transform.forward = transform.forward;
                if (PlayerPrefs.GetString("SoundSetting") == "On")
                {
                    AudioManager.instance.Play("CannonShotSound");
                }
            }
        }
    }
    
}