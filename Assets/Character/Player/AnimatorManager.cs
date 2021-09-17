using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    [SerializeField] int horizontal;
    [SerializeField] int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovment, float verticalMovement)
    {
        SetFloatHorizontal(horizontalMovment);
        SetFloatVertical(verticalMovement);
    }

    private void SetFloatVertical(float verticalMovement)
    {
        float snappedVertical;
        // Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
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


        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    // Snappped Horizontal
    private void SetFloatHorizontal(float horizontalMovment)
    {
        float snapppedHorizontal;

        if (horizontalMovment > 0 && horizontalMovment < 0.55f)
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
    }
}
