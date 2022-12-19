using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileProperties : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 3;
    [SerializeField] float projectileSelectDestructAfterThisSeconds = 3;
    [SerializeField] float colliderDetectorSphereRadius = 2f;
    
    [SerializeField] int elementType = 1;


    


    Vector3 targetLocation = Vector3.zero;


    private void Start()
    {
        Destroy(gameObject, projectileSelectDestructAfterThisSeconds);
    }

    private void Update()
    {
        InitiateFire();
        CheckIfHitEnemy();
    }

    private void CheckIfHitEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, colliderDetectorSphereRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.SendMessage("enemyHit", elementType);
                Destroy(gameObject);
            }
        }
    }

    private void InitiateFire()
    {
        if (targetLocation != Vector3.zero)
        {
            moveTowardTarget();
        }
    }


    private void MoveForward()
    {
        Vector3 MoveDirection = transform.forward;
        transform.position += MoveDirection * projectileSpeed * Time.deltaTime;
            
    }


    private void moveTowardTarget()
    {
        Vector3 MoveDirection = (transform.position - targetLocation).normalized * -1;
        transform.position += MoveDirection * projectileSpeed * Time.deltaTime;
    }

    public void setTargetLocation(Vector3 targetLocation)
    {
        this.targetLocation = targetLocation;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, colliderDetectorSphereRadius);
    }


}
