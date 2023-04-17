using System;
using Controllers.Level;
using Unity.VisualScripting;
using UnityEngine;

namespace Handlers
{
    public class ObjectDestroyer : MonoBehaviour
    {
        public Transform targetObject;

        void Update()
        {
            transform.position = targetObject.position - new Vector3(0,0,5);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (LevelPanel.Instance.comboCount >= 1f)
                {
                    LevelPanel.Instance.comboCount--;
                    Destroy(other.gameObject, 2f);
                }
                else
                {
                    Destroy(other.gameObject, 2f);
                }
            }

            if (other.CompareTag("Obstacle"))
            {
                Destroy(other.gameObject, 2f);
            }

            if (other.CompareTag("Treasure"))
            {
                Destroy(other.gameObject, 2f);
            }
        }
    }
}