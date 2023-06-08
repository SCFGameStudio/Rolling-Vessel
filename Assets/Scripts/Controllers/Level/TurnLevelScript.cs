using Controllers.Player;
using Managers;
using UnityEngine;

namespace Controllers.Level
{
    public class TurnLevelScript : MonoBehaviour
    {
        #region Instance

        

        
        private static TurnLevelScript _instance;

        public static TurnLevelScript Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("TurnLevelScript is null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        #endregion
    
        private Vector3 _currentEulerAngles;

        private void Update()
        {
            if (PlayerMovementController.Instance.IsRelentless) return;
            if (PlayerPhysicsController.Instance.AbleToMove == false) return;
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Moved) return;
            var touchDeltaX = touch.deltaPosition.x;
            _currentEulerAngles += new Vector3(0, 0, -touchDeltaX) * (Time.deltaTime * RotationManager.Instance.GetRotationSpeed());
            transform.localEulerAngles = _currentEulerAngles;

        }
    }
}