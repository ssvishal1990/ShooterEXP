using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Abiilities
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float acceptableDistanceFromObstacles = 1f;
    [SerializeField] LayerMask obstaclesLayerMask;

    private Vector3 previousTransformPosition;
    private Vector3 latestTransformPosition;
    

    protected override void Initialize()
    {
        base.Initialize();
        previousTransformPosition = transform.position;
        latestTransformPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        previousTransformPosition = transform.position;
        CheckVertical();
        CheckHorizontal();
        latestTransformPosition = transform.position;
        CheckIfPositionChangedAndActivateAnimation();

    }

    private void CheckIfPositionChangedAndActivateAnimation()
    {
        if (previousTransformPosition != latestTransformPosition)
        {
            SetMovementAnimationStatus(true);
        }
        else
        {
            SetMovementAnimationStatus(false);
        }
    }

    private void CheckHorizontal()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            if (!CheckIfNearWall(Vector3.right))
            {
                Vector3 newPosition = transform.position;
                newPosition.x += Time.deltaTime * moveSpeed;
                transform.position = newPosition;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!CheckIfNearWall(Vector3.left))
            {
                Vector3 newPosition = transform.position;
                newPosition.x -= Time.deltaTime * moveSpeed;
                transform.position = newPosition;
            }
        }
        //SetMovementAnimationStatus(false);
    }

    private void SetMovementAnimationStatus(bool status)
    {
     //   playerAnimator.SetBool("isMoving", status);
    }

    private void CheckVertical()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            if (!CheckIfNearWall(transform.forward))
            {
                Vector3 newPosition = transform.position;
                newPosition.z += Time.deltaTime * moveSpeed;
                transform.position = newPosition;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!CheckIfNearWall(-1 * transform.forward))
            {
                Vector3 newPosition = transform.position;
                newPosition.z -= Time.deltaTime * moveSpeed;
                transform.position = newPosition;
            }
        }
        //SetMovementAnimationStatus(false);
    }

    private bool CheckIfNearWall(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, acceptableDistanceFromObstacles, obstaclesLayerMask);
    }
}
