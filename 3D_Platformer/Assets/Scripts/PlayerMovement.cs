using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    private Animator animator;
    private int attackCounter = 0;

    public float controllerSensitivity = 1f;
    public Transform playerCamera;
    private float xRotation = 0f;


    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
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
        }
        if (Input.GetButtonUp("Fire2"))
        {
            animator.SetBool("isDefending", false);
        }

        // Attack
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isAttacking", true);
            attackCounter = (attackCounter + 1) % 2;
            animator.SetBool("Attack01", attackCounter == 0);
            animator.SetBool("Attack02", attackCounter == 1);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("Attack01", false);
            animator.SetBool("Attack02", false);
        }

        float joystickX = Input.GetAxis("RightStickHorizontal") * controllerSensitivity * Time.deltaTime;
        float joystickY = Input.GetAxis("RightStickVertical") * controllerSensitivity * Time.deltaTime;

        xRotation -= joystickY; // Invert Y axis for vertical look
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * joystickX); // Horizontal rotation
    }
}