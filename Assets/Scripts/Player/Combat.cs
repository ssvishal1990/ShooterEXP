using System;
using System.Collections;
using UnityEngine;
using Assets.Scripts.MouseWorld;


public class Combat : Abiilities
{
    [SerializeField] GameObject exoBody;
    [SerializeField] GameObject[] projectileLaunchPorts;
    [SerializeField] GameObject[] projectileType;
    [SerializeField] GameObject specialAttackProjectilePrefab;

    [SerializeField] protected float timeBetweenShots = 0.2f;

    public static event EventHandler<Vector3> onFireMainWeapon;
    public static event EventHandler<Vector3> onFireSpecialWeapon;
    public static event EventHandler onSpecialFireCameraShake;

    public float currentShotTimer;

    int currentPortInUse = 0;

    /// <summary>
    /// 0 For Fire
    /// 1 For ice
    ///  By default it's fire
    /// </summary>
    int currentProjectileTypeInUse = 0;


    Transform reticuleLocation = null;

        

    void Update()
    {
        reticuleLocation = MouseTracker.getAimReticulePosition();
        reticuleLocation.position = new Vector3(reticuleLocation.position.x, 0.5f, reticuleLocation.position.z);
        exoBody.transform.LookAt(reticuleLocation);
        //Debug.DrawLine(exoBody.transform.position, reticuleLocation.position, Color.red, 2f);
        SwitchProjectileType();


        Fire();
    }

    protected override void Initialize()
    {
        base.Initialize();
        currentShotTimer = timeBetweenShots;
    }

    private void SwitchProjectileType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentProjectileTypeInUse = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentProjectileTypeInUse = 1;
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            FireMainWeapon();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (ScoreSystem.instance.canLaunchSpecial())
            {
                GameObject specialAttackProjectile = Instantiate(specialAttackProjectilePrefab, projectileLaunchPorts[1].transform.position, Quaternion.identity);
                specialAttackProjectile.transform.forward = projectileLaunchPorts[1].transform.forward;
                soundSystem.playSpecialWeaponFireSound();
                onSpecialFireCameraShake?.Invoke(this, EventArgs.Empty);
                ScoreSystem.instance.specialLaunchScoreReduction();
                ScoreSystem.instance.specialLaunched();
            }
        }
    }

    private void FireMainWeapon()
    {
        currentShotTimer -= Time.deltaTime;
        if (currentShotTimer <= 0)
        {
            GameObject newPorjectileObject = Instantiate(projectileType[currentProjectileTypeInUse],
                                                            projectileLaunchPorts[currentPortInUse++ % projectileLaunchPorts.Length].transform.position,
                                                            Quaternion.identity);
            newPorjectileObject.TryGetComponent<ProjectileProperties>(out ProjectileProperties projectileProperties);
            projectileProperties.setTargetLocation(reticuleLocation.position);
            soundSystem.playMainWeaponFireSound();
            currentShotTimer = timeBetweenShots;
        }
        
    }
}
