using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSword : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.GetComponentInParent<EnemyMovement>().IsAlreadyHit)
        {
            var enemyMovement = other.GetComponentInParent<EnemyMovement>();
            enemyMovement.GetHit();
            enemyMovement.IsAlreadyHit = true; // Set the flag to true once the enemy is hit

            Destroy(other.transform.parent.gameObject, 1);
            GetComponentInParent<PlayerHealth>().IncreaseEnemyKills();

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }
}
