using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #region references
    public Camera mainCamera;
    private static CameraControl instance;
    #endregion

    #region orbit Variables
    private float xAngle;
    private float yAngle;
    [SerializeField]
    private float orbitSensitivity = 3f;

    private float maxYAngle = 30;
    private float minYAngle = -15;
    #endregion orbit Variables

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;

        mainCamera.transform.LookAt(transform);
    }


    void Update()
    {
        xAngle += Input.GetAxis("Mouse X") * orbitSensitivity;
        yAngle -= Input.GetAxis("Mouse Y") * orbitSensitivity;

        yAngle = ClampAngle(yAngle, minYAngle, maxYAngle);

        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);

        mainCamera.transform.LookAt(transform);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public static CameraControl Instance { get => instance; set => instance = value; }
}
