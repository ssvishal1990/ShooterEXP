using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    protected MeshRenderer onBodyMeshRenderer;
    protected NavMeshAgent navMeshAgent;
    protected Animator enemyAnimator;
    
    
    protected Transform playerGameObject;
    

    protected int damageValue;

    static protected bool nearPlayer = false;
    

    private void Awake()
    {
        InitComponents();
    }

    void Start()
    {

    }

    protected virtual void InitComponents()
    {
        //Debug.Log("Init Component called");
        onBodyMeshRenderer = GetComponent<MeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponent<Animator>();
        damageValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool PlayerCurrentVincinityInfoUpdate(bool status)
    {
        Debug.Log($"near player status {status}");
        navMeshAgent.isStopped = status;
        nearPlayer = status;
        return status;
    }



}
