using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; 
    public float walkSpeed = 2.0f;
    private Animator animator;
    public Transform swordTip;
    public bool IsAlreadyHit;

    public GameObject sword; // Assign the sword GameObject in the Inspector

    void StartAttack()
    {
        sword.GetComponent<Collider>().enabled = true; // Enable the sword collider
    }

    void EndAttack()
    {
        sword.GetComponent<Collider>().enabled = false; // Disable the sword collider
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        transform.Translate(directionToTarget * walkSpeed * Time.deltaTime, Space.World);

        // Set walking animation
        animator.SetBool("isWalking", true); 

        // Check for attack range
        if (Vector3.Distance(transform.position, target.position) < 3.0f) 
        {
            StartAttack();
            animator.SetBool("isAttacking", true);
            animator.SetBool("Attack01", true);// Trigger attack animation
            //CheckHit();
        }
        else
        {
            EndAttack();
            animator.SetBool("isAttacking", false);
            animator.SetBool("Attack01", false);
        }
    }

    //private void CheckHit()
    //{
    //    RaycastHit hit;
    //    float checkDistance = 0.5f; 

    //    if (Physics.Raycast(swordTip.position, swordTip.forward, out hit, checkDistance))
    //    {
    //        if (hit.collider.CompareTag("Player"))
    //        {
    //            hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
    //        }
    //    }
    //}

    public void GetHit()
    {
        animator.SetTrigger("Die");
    }
}
