using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public TextMeshProUGUI healthDisplay; 
    public TextMeshProUGUI enemyKillsDisplay;

    public float invincibilityDuration = 0.2f; // Time in seconds player is invincible after being hit
    private bool isInvincible = false;
    private float invincibilityTimer;

    private int enemyKills = 0;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible && !GetComponent<DogKnightMovement>().isDefending)
        {
            currentHealth -= damage;

            GetComponent<Animator>().SetTrigger("GetHit");
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GetComponent<Animator>().SetTrigger("Die");
                // Handle player death (disable movement, game over, etc.)
            }
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
            UpdateHealthDisplay();
        }

    }

    public void IncreaseEnemyKills()
    {
        enemyKills++;
        enemyKillsDisplay.text = "Enemy Kills: " + enemyKills;
    }

    private void UpdateHealthDisplay()
    {
        healthDisplay.text = "Health: " + currentHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }
}
