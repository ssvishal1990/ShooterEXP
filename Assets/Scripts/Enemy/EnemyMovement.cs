using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy
{
    [SerializeField] float enemyMoveSpeed = 5f;

    [SerializeField] Vector3 boxSize = Vector3.one;

    

    [SerializeField] LayerMask playerLayerMask;
    

    void Start()
    {
        
    }

    void Update()
    {
        if (!WithinAttackVincinity())
        {
            Move();
        }
    }

    private void Move()
    {
        navMeshAgent.SetDestination(playerGameObject.transform.position);
        if (navMeshAgent.speed != enemyMoveSpeed)
        {
            navMeshAgent.speed = enemyMoveSpeed;
        }
    }

    private bool WithinAttackVincinity()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize, Quaternion.identity, playerLayerMask);
        foreach (Collider collider in colliders)
        {
            //here this will engage attack;
            return PlayerCurrentVincinityInfoUpdate(true);
        }
        return PlayerCurrentVincinityInfoUpdate(false);
    }


    public void disableMovement()
    {
        enemyMoveSpeed = 0f;
        if (!navMeshAgent.isStopped)
        {
            navMeshAgent.isStopped = true;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    public void afterWave6IncreaseEnemySpeed(float increaseMoveSpeedBy)
    {
        enemyMoveSpeed += increaseMoveSpeedBy;
    }

    
}
