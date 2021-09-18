using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] Vector2 movementInput;
    [SerializeField] Vector2 cameraInput;

    private PlayerControls playerControls;
    private AnimatorManager animatorManager;
    private PlayerLocomotion playerLocomotion;

    public float moveAnount;

    public float cameraInputX;
    public float cameraInputY;
    public float verticalInput;
    public float horizontalInput;

    private bool isSprintingInput;
    private bool isWarkingInput;
    private bool isJumpInput;


    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprinting.performed += i => isSprintingInput = true;
            playerControls.PlayerActions.Sprinting.canceled += i => isSprintingInput = false;

            playerControls.PlayerActions.Walking.performed += i => isWarkingInput = true;
            playerControls.PlayerActions.Walking.canceled += i => isWarkingInput = false;

            playerControls.PlayerActions.Jump.performed += i => isJumpInput = true;
            playerControls.PlayerActions.Jump.canceled += i => isJumpInput = false;
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleWarkingInput();
        HandleJumpingInput();
        // HandleRunIActionnput
        // HandleActionInput

    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAnount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        var locomotionInput = new LocomotionInput();
        locomotionInput.IsSprinting = playerLocomotion.IsSprinting;
        locomotionInput.IsWarking = playerLocomotion.IsWarking;

        animatorManager.UpdateAnimatorValues(0, moveAnount, locomotionInput);
    }

    private void HandleSprintingInput()
    {
        if (isSprintingInput && moveAnount > 0.5f)
        {
            playerLocomotion.IsSprinting = true;
        }
        else
        {
            playerLocomotion.IsSprinting = false;
        }
    }

    private void HandleWarkingInput()
    {
        if (isWarkingInput && !isSprintingInput && moveAnount > 0.5f)
        {
            playerLocomotion.IsWarking = true;
        }
        else
        {
            playerLocomotion.IsWarking = false;
        }
    }

    public void HandleJumpingInput()
    {
        if (isJumpInput)
        {
            isJumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }

}

public class LocomotionInput
{
    public bool IsSprinting
    { get; set; }

    public bool IsWarking
    { get; set; }
}