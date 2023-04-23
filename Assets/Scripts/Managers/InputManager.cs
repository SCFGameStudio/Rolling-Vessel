using Controllers.Bullet;
using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [ShowInInspector] [Header("Data")] private InputData _data;

        private void Awake()
        {
            _data = GetInputData();
        }

        private static InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }

        private void UnSubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            // if (Input.GetMouseButtonDown(0) && PlayerPhysicsController.Instance.ableToMove)
            // {
            //     PlayerMovementController.Instance.IsReadyToMove(true);
            //     PlayerMovementController.Instance.IsReadyToPlay(true);
            //     BulletController.Instance.IsReadyToMove(true);
            //     BulletController.Instance.IsReadyToPlay(true);
            // }
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began || !PlayerPhysicsController.Instance.ableToMove) return;
            PlayerMovementController.Instance.IsReadyToMove(true);
            PlayerMovementController.Instance.IsReadyToPlay(true);
            BulletController.Instance.IsReadyToMove(true);
            BulletController.Instance.IsReadyToPlay(true);
        }
        private void OnReset()
        {
            PlayerMovementController.Instance.IsReadyToMove(false);
            PlayerMovementController.Instance.IsReadyToPlay(false);
            BulletController.Instance.IsReadyToMove(false);
            BulletController.Instance.IsReadyToPlay(false);
        }
    }
}
