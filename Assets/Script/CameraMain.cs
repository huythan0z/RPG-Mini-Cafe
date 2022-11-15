using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    //public float speedH = 2.0f;
    //public float speedV = 2.0f;

    //private float yah = 0.0f;
    //private float pitch = 0.0f;

    //public GameObject playerObj;
    //Vector3 cameraOffset;
    //void Start()
    //{
    //    cameraOffset = new Vector3(0, 4, -3);
    //}
    //void Update()
    //{
    //    yah += speedH * Input.GetAxis("Mouse X");
    //    pitch -= speedV * Input.GetAxis("Mouse Y");
    //    transform.eulerAngles = new Vector3(pitch, yah, 0.0f);

    //    transform.position = playerObj.transform.position + cameraOffset;
    //}


    public Transform PlayerTransform;
    private Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
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
