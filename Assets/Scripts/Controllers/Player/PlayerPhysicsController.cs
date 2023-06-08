using System.Collections;
using Controllers.Bullet;
using Controllers.Level;
using UnityEngine;
using Data.ValueObjects;
using Managers;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Instance

        private static PlayerPhysicsController _instance;

        public static PlayerPhysicsController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("PlayerPhysicsController is null");
                }

                return _instance;
            }
        }
        #endregion
        
        
        private void Awake()
        {
            _instance = this;
        }
        
        [SerializeField] private InvulnerabilityData _ınvulnerabilityData;
        [SerializeField] private new Collider collider;
        [SerializeField] private Material transparentShipMaterial;
        [SerializeField] private Material shipMaterial;
        [SerializeField] private Renderer shipRenderer;
        
        public bool IsInvulnerabilityAvailable = true;
        public bool AbleToMove = true;

        internal void GetInvulnerabilityData(InvulnerabilityData ınvulnerabilityData)
        {
            _ınvulnerabilityData = ınvulnerabilityData;
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if(collider.enabled && other.CompareTag("Enemy"))
            {
                CrashEnemy();
            }

            if (collider.enabled && other.CompareTag("Obstacle"))
            {
                CrashObstacle();
            }
            
            if (collider.enabled && other.CompareTag("Treasure"))
            {
                LevelPanel.Instance.Collect();
                if (PlayerPrefs.GetString("SoundSetting") == "On")
                {
                    AudioManager.instance.Play("TreasureCollectSound");
                }
                DestroyObject(other.gameObject);

            }
        }

        private void CrashObstacle()
        {
            PlayerMovementController.Instance.IsReadyToMove(false);
            PlayerMovementController.Instance.IsReadyToPlay(false);
            BulletController.Instance.IsReadyToMove(false);
            BulletController.Instance.IsReadyToPlay(false);
            AbleToMove = false;
            LevelPanel.Instance.StartGame(false);
            LevelPanel.Instance.Crash();
            
        }

        private void CrashEnemy()
        {
            PlayerMovementController.Instance.IsReadyToMove(false);
            PlayerMovementController.Instance.IsReadyToPlay(false);
            BulletController.Instance.IsReadyToMove(false);
            BulletController.Instance.IsReadyToPlay(false);
            AbleToMove = false;
            LevelPanel.Instance.StartGame(false);
            LevelPanel.Instance.Crash();
        }

        private void DestroyObject(GameObject objectsGameObject)
        {
            Destroy(objectsGameObject);
        }

        public IEnumerator Invulnerability()
        {
            IsInvulnerabilityAvailable = false;
            collider.enabled = false;
            shipRenderer.material = transparentShipMaterial;
            yield return new WaitForSeconds(_ınvulnerabilityData.InvulnerabilityDuration);
            collider.enabled = true;
            shipRenderer.material = shipMaterial;
            yield return new WaitForSeconds(2f);
            IsInvulnerabilityAvailable = true;
        }
    }
}