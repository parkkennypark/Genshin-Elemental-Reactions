using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform xPivot;
    public float sensitivity = 1;
    public float minX = -60f;
    public float maxX = 60f;
    float rotY = 0f;
    float rotX = 0f;

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        rotY += Input.GetAxis("Mouse X") * sensitivity;
        rotX += Input.GetAxis("Mouse Y") * sensitivity;

        rotX = Mathf.Clamp(rotX, minX, maxX);

        transform.localEulerAngles = new Vector3(0, rotY, 0);
        xPivot.transform.localEulerAngles = new Vector3(-rotX, 0, 0);
        // cameraTarget.position = Vector3.Lerp(cameraTarget.position, transform.position, Time.deltaTime * 12);
        transform.position = target.position;

        if (Cursor.visible && Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
