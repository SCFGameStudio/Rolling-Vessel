using UnityEngine;

public class TurnScript : MonoBehaviour
{
    private float rotationSpeed = 70;
    private Vector3 currentEulerAngles;
    private float z;
    public Transform pivotPoint;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentEulerAngles += new Vector3(0, 0, z) * Time.deltaTime * rotationSpeed;
            currentEulerAngles.z = Mathf.Clamp(currentEulerAngles.z, -16f, 16f);
            pivotPoint.localEulerAngles = currentEulerAngles;

        }

        if (Input.GetMouseButton(1))
        {
            currentEulerAngles += new Vector3(0, 0, -z) * Time.deltaTime * rotationSpeed;
            currentEulerAngles.z = Mathf.Clamp(currentEulerAngles.z, -16f, 16f);
            pivotPoint.localEulerAngles = currentEulerAngles;
        }
    }

    public void TurnX()
    {
        z = -1;
    }
}
