using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitInteractions : Abiilities
{
    
    // Start is called before the first frame update
    void Start()
    {
        EnemyAttack.enemyHitPlayer += EnemyAttack_onEnemyHitPlayer;        
    }

    private void EnemyAttack_onEnemyHitPlayer(object sender, int damageValue)
    {
        playerHealthSystem.takeDamage(damageValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takingDamage(int damageValue)
    {
        //Debug.Log($"Player Damaged with damage Value{damageValue}");
        playerHealthSystem.takeDamage(damageValue);

    }
}
