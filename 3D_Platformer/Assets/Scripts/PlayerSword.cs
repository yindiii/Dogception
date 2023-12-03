using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.GetComponent<EnemyMovement>().IsAlreadyHit)
        {
            var enemyMovement = other.GetComponent<EnemyMovement>();
            enemyMovement.GetHit();
            enemyMovement.IsAlreadyHit = true; // Set the flag to true once the enemy is hit

            Destroy(other.gameObject, 1);
            GetComponentInParent<PlayerHealth>().IncreaseEnemyKills();
        }
    }
}
