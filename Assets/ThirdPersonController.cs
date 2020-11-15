using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float distanceToGround;
    [SerializeField] private LayerMask groundMask;


    [Header("Movement")]
    public float jumpForce = 3f;
    public float speed = 6f;
    public float gravity = -9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator StopRolling(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        animator.SetBool("Roll", false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        if (isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;
            animator.SetBool("isJumping", false);
        }

        if (direction.magnitude > 0.1f)
        {
            animator.SetBool("isRunning", true);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("Roll", true);
            StartCoroutine(StopRolling(0.1f));
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping", true);
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
