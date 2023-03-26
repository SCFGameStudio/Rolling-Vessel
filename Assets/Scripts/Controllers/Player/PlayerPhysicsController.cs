using System;
using System.Collections;
using UnityEngine;
using Controllers.Player;
using Data.UnityObjects;

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
        
        //[SerializeField] private PlayerManager manager;
        [SerializeField] private CD_Player data;
        [SerializeField] private new Collider collider;


        private void Awake()
        {
            _instance = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(collider.enabled && other.CompareTag("Enemy"))
            {
                PlayerMovementController.Instance.StopPlayer();
                Debug.Log("düsmana carptin");
            }

            if (collider.enabled && other.CompareTag("Obstacle"))
            {
                PlayerMovementController.Instance.StopPlayer();
                Debug.Log("engele carptin");
            }
            
            if (collider.enabled && other.CompareTag("Treasure"))
            {
                
            }
        }


        public IEnumerator Invulnerability()
        {
            collider.enabled = false;
            yield return new WaitForSeconds(data.InvulnerabilityDuration);
            collider.enabled = true;
            yield return new WaitForSeconds(3f);
        }
    }
}