using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    [Header("Movement Speeds")]
    [SerializeField] float warkingSpeed = 1.5f;
    [SerializeField] float runningSpeed = 5f;
    [SerializeField] float sprintingSpeed = 7f;
    [SerializeField] float rotationSpeed = 15f;


    public bool IsSprinting
    { get; set; }
    public bool IsWarking
    { get; set; }


    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (IsWarking)
        {
            moveDirection = moveDirection * warkingSpeed;
        }
        else if (IsSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else if (inputManager.moveAnount >= 0.5f)
        {
            moveDirection = moveDirection * runningSpeed;
        }

        else
        {
            moveDirection = moveDirection * warkingSpeed;
        }



        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;

    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotaion = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotaion = Quaternion.Slerp(transform.rotation, targetRotaion, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotaion;

    }
}
