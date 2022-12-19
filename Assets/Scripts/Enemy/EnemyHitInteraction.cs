using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitInteraction : Enemy
{

    [SerializeField] ParticleSystem onHitFireParticleSystem;
    [SerializeField] ParticleSystem onHitIceParticleSystem;
    [SerializeField] ParticleSystem specialHitParticleSystem;


    protected int enemyHealth = 2;


    protected override void InitComponents()
    {
        base.InitComponents();
    }

    private void Update()
    {
        OnDeathActions();
    }

    private void OnDeathActions()
    {
        if (enemyHealth <= 0)
        {
            ScoreSystem.instance.addKillScore();
            WaveSystem.instance.reduceCurrentNumberOfEnemiesOnScreen();
            Destroy(gameObject);
        }
    }

    public void enemyHit(int elementIndex)
    {
        ScoreSystem.instance.addHitScore();
        switch (elementIndex)
        {
            case 1:
                onHitFireParticleSystem.Play();
                enemyHealth--;
                break;
            case 2:
                onHitIceParticleSystem.Play();
                enemyHealth--;
                break;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    public void instaDeath()
    {
        //Instantiate(specialHitParticleSystem, transform.position, Quaternion.identity);
        ScoreSystem.instance.addKillScore();
        enemyHealth = 0;
    }


}
