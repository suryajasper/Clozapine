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

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
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
}
