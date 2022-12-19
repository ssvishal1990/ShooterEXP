using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : Abiilities
{
    [SerializeField] Material highHealth;
    [SerializeField] Material mediumHealth;
    [SerializeField] Material lowHealth;
    [SerializeField] GameObject playerHealthIndicatingBody;
    [SerializeField] float healthRegenerationAfterTimeTimer = 3f;

    public int currentHealth = totalHealthValue;


    IEnumerator healthRegenerateCoroutine;

    protected float currentTime = 0f;

    bool canRegenerateNow = false;
    bool healthRegenCooroutineStarted = false;

    internal void takeDamage(int damageValue)
    {
        StopCoroutine(startHealthRegen());
        healthRegenCooroutineStarted = false;
        canRegenerateNow = false;
        currentTime = 0f;
        currentHealth -= damageValue;
        UpdateHealthMaterialAsPerCurrentHealth();
    }

    private void UpdateHealthMaterialAsPerCurrentHealth()
    {
        if (currentHealth > 6)
        {
            playerHealthIndicatingBody.GetComponent<MeshRenderer>().material = highHealth;
        }
        else if (currentHealth > 3 && currentHealth <= 6)
        {
            playerHealthIndicatingBody.GetComponent<MeshRenderer>().material = mediumHealth;
        }
        else if (currentHealth <= 3 && currentHealth > 0)
        {
            playerHealthIndicatingBody.GetComponent<MeshRenderer>().material = lowHealth;
        }
        else if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");//Need to create a gameover scene;
            playerHealthIndicatingBody.GetComponent<MeshRenderer>().material = lowHealth;
        }
    }


    private void Update()
    {
        waitBeforeRegeneration();
        if (currentHealth < totalHealthValue && canRegenerateNow && !healthRegenCooroutineStarted)
        {
            StartCoroutine(startHealthRegen());
        }
    }

    IEnumerator startHealthRegen()
    {
        healthRegenCooroutineStarted = true;
        while (currentHealth <= totalHealthValue)
        {
            yield return new WaitForSecondsRealtime(1f);
            currentHealth++;
            UpdateHealthMaterialAsPerCurrentHealth();
        }
    }


    protected void waitBeforeRegeneration()
    {
        currentTime += Time.deltaTime;
        if (currentTime > healthRegenerationAfterTimeTimer)
        {
            canRegenerateNow = true;
        }
    }
}
