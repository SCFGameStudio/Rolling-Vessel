using UnityEngine;
using UnityEngine.UI;

namespace Handlers
{
    public class UIEventHandler : MonoBehaviour
    {
        public delegate void ButtonClickedEventHandler();

        public static event ButtonClickedEventHandler Button1Clicked;
        public static event ButtonClickedEventHandler Button2Clicked;

        private Button button1;
        private Button button2;

        private void Awake()
        {
            button1 = transform.Find("Button1").GetComponent<Button>();
            button1.onClick.AddListener(OnButton1Click);

            button2 = transform.Find("Button2").GetComponent<Button>();
            button2.onClick.AddListener(OnButton2Click);
        }

        private void OnButton1Click()
        {
            if (Button1Clicked != null)
            {
                Button1Clicked();
            }
        }

        private void OnButton2Click()
        {
            if (Button2Clicked != null)
            {
                Button2Clicked();
            }
        }
    }
}