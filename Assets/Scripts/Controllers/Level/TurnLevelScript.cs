using System;
using System.Collections.Generic;
using Controllers.Player;
using Managers;
using UnityEngine;

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
    
    //[SerializeField]private float rotationSpeed = 80;
    private Vector3 currentEulerAngles;

    private void Update()
    {
        if (PlayerMovementController.Instance.isRelentless == false)
        {
            if (Input.GetMouseButton(0) && PlayerPhysicsController.Instance.ableToMove == true)
            {
                float mouseX = Input.GetAxis("Mouse X");
                currentEulerAngles += new Vector3(0, 0, -mouseX) * Time.deltaTime * RotationManager.Instance.GetRotationSpeed();
                transform.localEulerAngles = currentEulerAngles;
            }
        }
    }
}