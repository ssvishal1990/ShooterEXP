using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Animator playerAnimator;

    static protected int totalHealthValue = 9;

    protected HealthSystem playerHealthSystem;
    protected PlayerHitInteractions playerHitInteractions;
    protected Combat combat;
    protected Movement movement;
    protected SoundSystem soundSystem;

    private void Awake()
    {
        Initialize();
    }

    void Start()
    {

        //Initialize();
    }

    protected virtual void Initialize()
    {
        //Debug.Log("Initialize player health components");
        playerHealthSystem = GetComponent<HealthSystem>();
        playerHitInteractions = GetComponent<PlayerHitInteractions>();
        combat = GetComponent<Combat>();
        movement = GetComponent<Movement>();
        soundSystem = FindObjectOfType<SoundSystem>();
    }

    public Animator getPlayerAnimator()
    {
        return playerAnimator;
    }


}
