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


    [Header("Movement Flag")]
    [SerializeField] public bool IsSprinting;
    [SerializeField] public bool IsWarking;
    [SerializeField] public bool isGrounded;

    [Header("Falling")]
    [SerializeField] float inAirTimer = 0f;
    [SerializeField] float leapingVelocity = 3;
    [SerializeField] float fallingVelocity = 33;
    [SerializeField] LayerMask goundLayer;
    [SerializeField] float raycastHeightOffset = 0.5f;



    private InputManager inputManager;
    private Vector3 moveDirection;
    private Transform cameraObject;
    private Rigidbody playerRigidbody;
    private PlayerManager playerManager;
    private AnimatorManager animatorManager;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting) return;
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

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y = raycastOrigin.y + raycastHeightOffset;
        if (!isGrounded)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }
            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);

        }
        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, goundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            inAirTimer = 0f;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
