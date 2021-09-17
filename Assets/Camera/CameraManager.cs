using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform targetTranform;
    [SerializeField] Transform cameraPivot;
    [SerializeField] float cameraFollowSpeed = 0.2f;
    [SerializeField] float cameraLookSpeed = 2;
    [SerializeField] float cameraPivotSpeed = 2;
    [SerializeField] float lookAngle;
    [SerializeField] float pivotAngle;
    [SerializeField] float minimumPivotAngle = -35;
    [SerializeField] float maximumPivotAngle = 35;

    private InputManager inputManager;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    private void Awake()
    {
        targetTranform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotationCamera();
    }

    private void FollowTarget()
    {
        Vector3 targetPositon = Vector3.SmoothDamp(transform.position, targetTranform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPositon;
    }

    private void RotationCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;

    }
}
