using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] Vector2 movementInput;

    private PlayerControls playerControls;
    private AnimatorManager animatorManager;

    public float verticalInput;
    public float horizontalInput;

    private float moveAnount;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            //playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        movementInput.x = playerControls.PlayerMovement.Movement.ReadValue<Vector2>().x;
        movementInput.y = playerControls.PlayerMovement.Movement.ReadValue<Vector2>().y;
    }

    public void HandleAllInput()
    {
        HandleMovementInput();
        // HandleJumpInput
        // HandleRunIActionnput
        // HandleActionInput

    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAnount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAnount);
    }

   }
