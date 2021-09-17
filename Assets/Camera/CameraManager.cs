using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform targetTranform;
    [SerializeField] float cameraFollowSpeed = 0.2f;

    private Vector3 cameraFollowVelocity = Vector3.zero;

    private void Awake()
    {
        targetTranform = FindObjectOfType<PlayerManager>().transform;
    }

    public void FollowTarget()
    {
        Vector3 targetPositon = Vector3.SmoothDamp(transform.position, targetTranform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPositon;
    }
}
