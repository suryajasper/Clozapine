using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 velocity;

    public Camera cam;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float distanceToGround;
    public LayerMask groundMask;

    [Header("Movement")]
    public float jumpForce = 3f;
    public float speed = 12f;
    public float gravity = -9.81f;

    private bool isClimbing;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Climbable"))
        {
            cam.GetComponent<cameraScript>().OffsetForwardPosition(-3f);
            isClimbing = true;
            animator.SetBool("isClimbing", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Climbable"))
        {
            cam.GetComponent<cameraScript>().OffsetForwardPosition(3f);
            isClimbing = false;
            animator.SetBool("isClimbing", false);
        }
    }

    private void Move()
    {
        cam.GetComponent<cameraScript>().enabled = true;
        bool isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animator.SetFloat("speed", Mathf.Abs(z));

        Vector3 move = (transform.right * x + transform.forward * z) * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        controller.Move(move * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Climb()
    {
        cam.GetComponent<cameraScript>().enabled = false;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.up * y) * 2f;

        if (Input.GetButtonDown("Jump"))
        {
            move += transform.up * -10f;
        }

        controller.Move(move * Time.deltaTime);
    }

    void Update()
    {
        if (isClimbing)
            Climb();
        else
            Move();
    }
}
