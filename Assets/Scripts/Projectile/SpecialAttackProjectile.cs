using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackProjectile : MonoBehaviour
{
    [SerializeField] float specialAttackProjectileSpeed = 10f;
    [SerializeField] float projectileSelectDestructAfterThisSeconds = 3;
    [SerializeField] float colliderDetectorSphereRadius = 3f;
    [SerializeField] GameObject specialAttackParticleSystem;

    List<Collider> colliders = new List<Collider>();

    protected bool specialAttackParticleSystemDeployed = false;

    void Start()
    {
        Invoke("destroyAllEnemies", projectileSelectDestructAfterThisSeconds);
        //Destroy(gameObject, projectileSelectDestructAfterThisSeconds);
    }


    void Update()
    {
        
        transform.position += transform.forward * specialAttackProjectileSpeed * Time.deltaTime;
        CheckIfHitEnemy();
    }

    private void CheckIfHitEnemy()
    {
        Collider[] tempColliders = Physics.OverlapSphere(transform.position, colliderDetectorSphereRadius);
        foreach (Collider tempCollider in tempColliders)
        {
            if (tempCollider.gameObject.tag == "Enemy")
            {
                colliders.Add(tempCollider);
                tempCollider.gameObject.SendMessage("disableMovement");
                DeploySpecialAttackParticleSystem(tempCollider);
            }
        }
    }

    private void DeploySpecialAttackParticleSystem(Collider tempCollider)
    {
        if (!specialAttackParticleSystemDeployed)
        {
            Debug.Log("Deplay special attack particleSystem");
            Vector3 tempPosition = tempCollider.transform.position;
            tempPosition.y = 0;
            Instantiate(specialAttackParticleSystem, tempCollider.transform.position, Quaternion.identity);
            specialAttackParticleSystemDeployed = true;
        }
    }

    private void destroyAllEnemies()
    {
        foreach (Collider collider in colliders)
        {

            if (collider != null && collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.SendMessage("instaDeath");
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, colliderDetectorSphereRadius);
    }
}
