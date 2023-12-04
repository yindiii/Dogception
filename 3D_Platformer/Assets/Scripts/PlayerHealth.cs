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
    public GameObject parkourPlatforms;
    public GameObject hidden;
    private int spotsVisited = 0;

    private AudioSource audioSource;
    public AudioClip secret;
    public AudioClip reveal;
    public AudioClip hit;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible && !GetComponent<DogKnightMovement>().isDefending)
        {
            currentHealth -= damage;
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.clip = hit;
                audioSource.Play();
            }
            GetComponent<Animator>().SetTrigger("GetHit");
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GetComponent<Animator>().SetTrigger("Die");
                GameManager.Instance.ShowLosingScreen();
                //if (audioSource != null && audioSource.clip != null)
                //{
                //    audioSource.clip = die;
                //    audioSource.Play();
                //}
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
        if (enemyKills >= 20)
        {
            parkourPlatforms.SetActive(true);
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.clip = reveal;
                audioSource.Play();
            }
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SecretSpot") && !other.GetComponent<SecretSpot>().isVisited)
        {
            other.GetComponent<SecretSpot>().isVisited = true;
            spotsVisited++;

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.clip = secret;
                audioSource.Play();
            }

            if (spotsVisited >= 4)
            {
                hidden.SetActive(true);
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.clip = reveal;
                    audioSource.Play();
                }
            }
        }

        if (other.CompareTag("Goal"))
        {
            GameManager.Instance.ShowWinningScreen();
        }
    }
}
