using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private Animator playerAnimator;
    private AudioSource audioSource;

    void Start()
    {
        // Assuming the shield's parent is the player
        playerAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemySword") && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Defend"))
        {
            // Successful parry
            EnemyMovement enemy = other.GetComponentInParent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.TriggerDizzyAnimation();
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
