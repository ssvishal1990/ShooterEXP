using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : Enemy
{
    void Start()
    {
        
    }

    
    void Update()
    {
        enemyAnimator.SetBool("PlayerWithinRange", nearPlayer);
    }
}
