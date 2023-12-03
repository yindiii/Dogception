using UnityEngine;

public class DogKnightMovement : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float jumpForce = 5.0f;
    private bool isGrounded;
    private Animator animator;
    private int attackCounter = 0;
    public bool isDefending;
    public Transform swordTip;

    public float controllerSensitivity = 1f;
    public Transform playerCamera;
    private float xRotation = 0f;

    private Transform currentPlatform = null;

    public LayerMask groundLayer; // Assign this in the Inspector
    private BoxCollider bodyCollider;
    private BoxCollider feetCollider;


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
        Cursor.lockState = CursorLockMode.Locked;
        bodyCollider = GetComponent<BoxCollider>();
        feetCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;
        forward.y = 0;
        forward.Normalize();

        // Combine forward and right based on player input
        Vector3 movement = (forward * v + right * h).normalized;

        // Apply movement
        transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);

        // Determine if the player is walking, running, or idle
        if (movement.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", Input.GetAxis("LeftTrigger") > 0.1f);
            transform.Translate(movement * (Input.GetAxis("LeftTrigger") > 0.1f ? runSpeed : walkSpeed) * Time.deltaTime, Space.World);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        // Defend
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("isDefending", true);
            isDefending = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            animator.SetBool("isDefending", false);
            isDefending = false;
        }

        // Attack
        if (Input.GetButtonDown("Fire1"))
        {
            StartAttack();
            animator.SetBool("isAttacking", true);
            attackCounter = (attackCounter + 1) % 2;
            animator.SetBool("Attack01", attackCounter == 0);
            animator.SetBool("Attack02", attackCounter == 1);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            EndAttack();
            animator.SetBool("isAttacking", false);
            animator.SetBool("Attack01", false);
            animator.SetBool("Attack02", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (currentPlatform != null)
        {
            transform.position += currentPlatform.GetComponent<MovingPlatformScript>().GetPlatformMovement();
        }

        float joystickX = Input.GetAxis("RightStickHorizontal") * controllerSensitivity * Time.deltaTime;
        float joystickY = Input.GetAxis("RightStickVertical") * controllerSensitivity * Time.deltaTime;

        xRotation -= joystickY; // Invert Y axis for vertical look
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * joystickX); // Horizontal rotation

        CheckGrounded();
        //CheckHit();
    }

    //void FixedUpdate()
    //{
        //RaycastHit hit;
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f) && hit.collider.CompareTag("Ground");
    //}

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has landed on a 'Ground' tagged object
        if (collision.gameObject.CompareTag("Ground"))
        {
            // If the ground object is a moving platform, store a reference to it
            if (collision.gameObject.GetComponent<MovingPlatformScript>() != null)
            {
                currentPlatform = collision.transform;
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        // Check if the player has left a 'Ground' tagged object
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentPlatform = null;
        }
    }

    void CheckGrounded()
    {
        // Check if the feet collider is touching the ground
        isGrounded = Physics.CheckBox(feetCollider.bounds.center, feetCollider.bounds.extents, Quaternion.identity, groundLayer);
    }

    //private void CheckHit()
    //{
    //    RaycastHit hit;
    //    float checkDistance = 0.5f; 

    //    if (Physics.Raycast(swordTip.position, swordTip.forward, out hit, checkDistance))
    //    {
    //        if (hit.collider.CompareTag("Enemy"))
    //        {
    //            hit.collider.gameObject.GetComponent<EnemyMovement>().GetHit();
    //            Destroy(hit.collider.gameObject, 1);
    //            GetComponent<PlayerHealth>().IncreaseEnemyKills();
    //        }
    //    }
    //}
}