using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>().TakeDamage(1);

            //if (audioSource != null && audioSource.clip != null)
            //{
            //    audioSource.Play();
            //}
        }
    }
}
