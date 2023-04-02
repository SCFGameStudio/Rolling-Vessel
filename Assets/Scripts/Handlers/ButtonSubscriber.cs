using Controllers.Player;
using UnityEngine;

namespace Handlers
{
    public class ButtonSubscriber : MonoBehaviour
    {
        private void OnEnable()
        {
            UIEventHandler.Button1Clicked += OnButton1Clicked;
            UIEventHandler.Button2Clicked += OnButton2Clicked;
        }

        private void OnDisable()
        {
            UIEventHandler.Button1Clicked -= OnButton1Clicked;
            UIEventHandler.Button2Clicked -= OnButton2Clicked;
        }

        public void OnButton1Clicked()
        {
            PlayerMovementController.Instance.RelentlessSkill();
        }

        public void OnButton2Clicked()
        {
            PlayerMovementController.Instance.InvulnerabilitySkill();
        }
    }
}