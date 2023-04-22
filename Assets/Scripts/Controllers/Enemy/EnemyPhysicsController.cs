using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cannon"))
            {
                Destroy(gameObject);
            }
        }
    }
}