using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] // Will Automatically Add Enemy Script When This Script Is Attached To Any GameObject.
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHealth = 5;
    
    [Tooltip("Increases Enemy HitPoints(Health) When Enemy Is Killed")]
    [SerializeField] int difficultyRamp = 1;

    int currHealth;

    Enemy enemy;

    void Start(){
        enemy = FindObjectOfType<Enemy>();
    }

    void OnEnable()
    {
        currHealth=maxHealth;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currHealth--;
        if (currHealth < 1){
            enemy.Reward();
            gameObject.SetActive(false);
            maxHealth += difficultyRamp;
        }
    }
}
