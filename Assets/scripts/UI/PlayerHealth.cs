using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;

    public bool isDead = false;

    [Header("Health Bar")]
    // A public so we can change the max health health in the inspector
    private float health;
    // used to animated healthbar
    private float lerpTimer;


    [Header("Health Bar")]
    // max health for characcter
    public float maxHealth = 100;
    // speed set for when the health is chipped away from character
    public float chipSpeed = 2f;
    // Healthbar gameobjects
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    // Damage Overlay gameobject  
    public Image overlay;
    // how lon the image stays fully opaque
    public float duration;
    // how long the image will fade
    public float fadespeed;

    // timer to check against the duration
    private float durationTimer;


    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        // At the start of the game the health of the character will be at its maxed
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0); // image will not show at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            health = 0;
        }

        // clamp the health so its never below zero or rises above the max health
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        // Fuction for damage overlay
        if (overlay.color.a > 0)
        {
            //pauses duration timer so when health below 30 overlay will stay
            if (health < 30)
                return;
            duration += Time.deltaTime;
            if (duration < duration)
            {
                // fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadespeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha); // decrement the temp value
            }
        }
    }
    public void UpdateHealthUI()
    {
        Debug.Log(health);
        // store local variables for the fill amount of the healthbars
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        //local variable for health
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplte = lerpTimer / chipSpeed;
            percentComplte = percentComplte * percentComplte;
            // lerp the time for the fill amount of the back healthbar
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplte);

        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }
    //Take Damage function
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            // subtracts damage to health 
            health -= damage;
            // Reset lerp timer
            lerpTimer = 0f;
            durationTimer = 0; // resest timer when damage taken
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1); // decrement the temp value
        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        health = 0;
        isDead = true;
        Debug.Log("Player Dead");
    }

    // Restore health function
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
