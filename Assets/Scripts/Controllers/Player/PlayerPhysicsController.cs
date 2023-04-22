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
        
        [SerializeField] private InvulnerabilityData _ınvulnerabilityData;
        [SerializeField] private new Collider collider;
        public bool isInvulnerabilityAvailable = true;
        public bool ableToMove = true;

        internal void GetInvulnerabilityData(InvulnerabilityData ınvulnerabilityData)
        {
            _ınvulnerabilityData = ınvulnerabilityData;
        }


        private void Awake()
        {
            _instance = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(collider.enabled && other.CompareTag("Enemy"))
            {
                PlayerMovementController.Instance.StopPlayer();
                PlayerMovementController.Instance.IsReadyToMove(false);
                PlayerMovementController.Instance.IsReadyToPlay(false);
                BulletController.Instance.IsReadyToMove(false);
                BulletController.Instance.IsReadyToPlay(false);
                ableToMove = false;
                LevelPanel.Instance.StartGame(false);
                LevelPanel.Instance.OnCrash();
                Debug.Log("düsmana carptin");
            }

            if (collider.enabled && other.CompareTag("Obstacle"))
            {
                PlayerMovementController.Instance.StopPlayer();
                PlayerMovementController.Instance.IsReadyToMove(false);
                PlayerMovementController.Instance.IsReadyToPlay(false);
                BulletController.Instance.IsReadyToMove(false);
                BulletController.Instance.IsReadyToPlay(false);
                ableToMove = false;
                LevelPanel.Instance.StartGame(false);
                LevelPanel.Instance.OnCrash();
                Debug.Log("engele carptin");
            }
            
            if (collider.enabled && other.CompareTag("Treasure"))
            {
                Debug.Log("Treasure!");
                LevelPanel.Instance.OnCollect();
                DestroyObject(other.gameObject);

            }
        }

        private void DestroyObject(GameObject objectsGameObject)
        {
            
            Destroy(objectsGameObject);
        }


        public IEnumerator Invulnerability()
        {
            isInvulnerabilityAvailable = false;
            collider.enabled = false;
            yield return new WaitForSeconds(_ınvulnerabilityData.InvulnerabilityDuration);
            collider.enabled = true;
            yield return new WaitForSeconds(2f);
            isInvulnerabilityAvailable = true;
        }
        
        internal void OnReset()
        {
            ableToMove = true;
        }
    }
}