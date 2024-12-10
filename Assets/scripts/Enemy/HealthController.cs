using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField]
    private Animator anim; // Reference to the Animator component
    private string hitAnimationName = "isHit"; // Name of the hit animation parameter in the Animator
    private string deathAnimationName = "isDead"; // Name of the death animation parameter in the Animator

    private UnityEngine.AI.NavMeshAgent agent; // Reference to the NavMeshAgent component


    // Add a health variable to keep track of the enemy's current health.
    public float health => currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Play the hit animation randomly with a 50% chance when taking damage.
        if (UnityEngine.Random.value <= 0.5f)
        {
            anim.SetTrigger(hitAnimationName);
        }

        // Log the current health to see if it's decreasing.
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Play the death animation when the enemy dies.
        anim.SetTrigger("isDead");

        // Disable the NavMeshAgent to stop enemy movement.
        if (agent != null)
        {
            agent.enabled = false;
        }

        // Implement the behavior when the enemy dies here, e.g., destroy the GameObject after a delay.
        Destroy(gameObject, 5f);
    }
}



