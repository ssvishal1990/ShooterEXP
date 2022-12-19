using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Enemy
{
    public static event EventHandler<int> enemyHitPlayer;

    [SerializeField] float sphereRadius = 0.5f;
    [SerializeField] LayerMask playerLayerMask;


    public bool testnearPlayer = false;
    
    int waveDamageValue;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WithinAttackVincinity();
    }

    private void WithinAttackVincinity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius, playerLayerMask);
        foreach (Collider collider in colliders)
        {
            //here this will engage attack;
            testnearPlayer = true;
            return;
        }
        testnearPlayer = false;
        return;
    }

    public void HitPlayer()
    {
        Debug.Log($"Inside hit player with player in range -> {nearPlayer}");
        if (testnearPlayer)
        {
            enemyHitPlayer?.Invoke(this, damageValue);
            //playerGameObject.GetComponent<PlayerHitInteractions>().takingDamage(damageValue);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }


}
