using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    [SerializeField] int horizontal;
    [SerializeField] int vertical;

    private float snappedVertical = 0f;
    private float snapppedHorizontal = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMovment, float verticalMovement, LocomotionInput locomotionInput)
    {
        // Snapped Vertical
        if (locomotionInput.IsSprinting)
        {
            snappedVertical = 2f;
        }
        else if (locomotionInput.IsWarking)
        {
            snappedVertical = 0.25f;
        }
        else if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement >= 0.55f)
        {
            snappedVertical = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement <= -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0;
        }
        // Snappped Horizontal

        if (locomotionInput.IsSprinting)
        {
            snapppedHorizontal = horizontalMovment;
        }
        else if (locomotionInput.IsWarking)
        {
            snapppedHorizontal = 0.25f;
        }
        else if (horizontalMovment > 0 && horizontalMovment < 0.55f)
        {
            snapppedHorizontal = 0.5f;
        }
        else if (horizontalMovment > 0.55f)
        {
            snapppedHorizontal = 1f;
        }
        else if (horizontalMovment < 0 && horizontalMovment > -0.55f)
        {
            snapppedHorizontal = -0.5f;
        }
        else if (horizontalMovment < -0.55f)
        {
            snapppedHorizontal = -1f;
        }
        else
        {
            snapppedHorizontal = 0;
        }

        animator.SetFloat(horizontal, snapppedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

}
