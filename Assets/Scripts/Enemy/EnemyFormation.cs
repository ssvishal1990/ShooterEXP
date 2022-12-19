using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : Enemy
{
    [SerializeField] GameObject enemyBody;
    
    void Start()
    {
        
    }

    void Update()
    {
        enemyBody.transform.rotation = transform.rotation;
    }
}
