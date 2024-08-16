using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem weaponParticles;
    [SerializeField] float maxTargetDist = 15;
    Transform target;


    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDist = Mathf.Infinity;

        foreach(Enemy enemy in enemies){

            float targetDist = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDist < maxDist){
                maxDist = targetDist;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    void AimWeapon()
    {
        float enemyDist = Vector3.Distance(transform.position, target.transform.position);
        weapon.LookAt(target);
        if(enemyDist<=maxTargetDist) Attack(true);
        else Attack(false);
    }

    void Attack(bool isAttacking){ 
        var emm = weaponParticles.emission;
        emm.enabled = isAttacking;
    }

}
