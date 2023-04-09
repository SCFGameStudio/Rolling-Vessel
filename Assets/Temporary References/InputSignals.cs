using System;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoBehaviour
    {
        private static InputSignals _instance;

        public static InputSignals Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("InputSignal is null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
        
        
    }
}