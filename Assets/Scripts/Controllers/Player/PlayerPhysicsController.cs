using System.Collections;
using Controllers.Bullet;
using Controllers.Level;
using UnityEngine;
using Data.ValueObjects;

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
                DestroyObject(other.gameObject);

            }
        }

        private void CrashObstacle()
        {
            PlayerMovementController.Instance.StopPlayer();
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
            PlayerMovementController.Instance.StopPlayer();
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
            yield return new WaitForSeconds(_ınvulnerabilityData.InvulnerabilityDuration);
            collider.enabled = true;
            yield return new WaitForSeconds(2f);
            IsInvulnerabilityAvailable = true;
        }
        
        // internal void OnReset()
        // {
        //     AbleToMove = true;
        // }
    }
}