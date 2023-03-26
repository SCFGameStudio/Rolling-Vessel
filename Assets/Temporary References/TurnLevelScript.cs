using System;
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
    
    [SerializeField]private float rotationSpeed = 80;
    private Vector3 currentEulerAngles;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentEulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * rotationSpeed;
            transform.localEulerAngles = currentEulerAngles;
        }

        if (Input.GetMouseButton(1))
        {
            currentEulerAngles += new Vector3(0, 0, -1) * Time.deltaTime * rotationSpeed;
            transform.localEulerAngles = currentEulerAngles;
        }
    }
}