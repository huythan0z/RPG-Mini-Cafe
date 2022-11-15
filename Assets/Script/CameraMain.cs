using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 _cameraOffset;
    public float SmoothFactor = 0.5f;
    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationSpeed = 5.0f;
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }
    void LateUpdate()
    {
        if (RotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            _cameraOffset = camTurnAngle * _cameraOffset;
        }
        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        if (LookAtPlayer || RotateAroundPlayer)
        {
            transform.LookAt(PlayerTransform);
        }
    }
}
