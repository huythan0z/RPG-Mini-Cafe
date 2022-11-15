using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] Button btnJump;
    [SerializeField] Button btnSit;

    Animator aniCharacter;
    Rigidbody objRigitBody;
    bool sitting = false;
    void Start()
    {
        aniCharacter = GetComponent<Animator>();
        objRigitBody = transform.GetComponent<Rigidbody>();
        btnJump.onClick.AddListener(PlayerJump);
        btnSit.onClick.AddListener(PlayerSit);
    }
    void Update()
    {
        //MoveWeb();
        MoveMobile();
    }

    void MoveWeb()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            aniCharacter.SetBool("Run", false);
            aniCharacter.SetBool("Sit", false);
            aniCharacter.SetBool("Stop", true);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            aniCharacter.SetBool("Stop", false);
            aniCharacter.SetBool("Sit", false);
            aniCharacter.SetBool("Run", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 playerPositionUp = transform.position;
            playerPositionUp.z += Vector3.forward.z * Time.deltaTime * 2.0f;
            transform.position = playerPositionUp;


            transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 playerPositionDown = transform.position;
            playerPositionDown.z += Vector3.back.z * Time.deltaTime * 2.0f;
            transform.position = playerPositionDown;
            transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.back);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 playerPositionRight = transform.position;
            playerPositionRight.x += Vector3.right.x * Time.deltaTime * 2.0f;
            transform.position = playerPositionRight;
            transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 playerPositionLeft = transform.position;
            playerPositionLeft.x += Vector3.left.x * Time.deltaTime * 2.0f;
            transform.position = playerPositionLeft;
            transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            objRigitBody.AddForce(new Vector3(0, 3.5f, 0), ForceMode.VelocityChange);

            aniCharacter.SetBool("Sit", false);
            aniCharacter.SetBool("Stop", true);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            aniCharacter.SetBool("Run", false);
            aniCharacter.SetBool("Stop", false);
            aniCharacter.SetBool("Sit", true);
        }
    }
    void MoveMobile()
    {
        objRigitBody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, objRigitBody.velocity.y, _joystick.Vertical * _moveSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(objRigitBody.velocity);
            aniCharacter.SetBool("Run", true);
            aniCharacter.SetBool("Stop", false);
            aniCharacter.SetBool("Sit", false);
            sitting = false;
        }
        else
        {
            if (sitting == true)
            {
                aniCharacter.SetBool("Stop", false);
                aniCharacter.SetBool("Run", false);
                aniCharacter.SetBool("Sit", true);
            }
            else
            {
                aniCharacter.SetBool("Stop", true);
                aniCharacter.SetBool("Run", false);
            }
        }
    }
    void PlayerJump()
    {
        objRigitBody.AddForce(new Vector3(0, 4.0f, 0), ForceMode.VelocityChange);
        aniCharacter.SetBool("Sit", false);
        aniCharacter.SetBool("Run", false);
        aniCharacter.SetBool("Stop", true);
        sitting = false;
    }
    void PlayerSit()
    {
        sitting = true;
    }
}
