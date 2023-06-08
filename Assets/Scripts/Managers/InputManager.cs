using Controllers.Bullet;
using Controllers.Player;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began || !PlayerPhysicsController.Instance.AbleToMove) return;
            PlayerMovementController.Instance.IsReadyToMove(true);
            PlayerMovementController.Instance.IsReadyToPlay(true);
            BulletController.Instance.IsReadyToMove(true);
            BulletController.Instance.IsReadyToPlay(true);
        }
    }
}
