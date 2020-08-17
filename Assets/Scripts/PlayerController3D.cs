using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerController3D : MonoBehaviour
{
    public GameObject multiplayerGraphics;
    public GameObject ragdollPrefab;

    private CharacterController controller;
    private Vector3 velocity;
    private float counteractYVelocity = -2f;

    public animationManager animManager;
    public Camera cam;
    public GunManager gunManager;
    [HideInInspector] public Animator gunPositionAnimator;

    [Header("Damage")]
    public float health;
    public Collider head;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float distanceToGround;
    [SerializeField] private LayerMask groundMask;

    [Header("Movement")]
    public float jumpForce = 3f;
    public float speed = 12f;
    public float gravity = -9.81f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        if (isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = counteractYVelocity;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        Vector3 move = (transform.right * x + transform.forward * z) * speed * (Input.GetKey(KeyCode.LeftShift) ? 1.25f: 1f);
        if (gunPositionAnimator.GetBool("Sight"))
            move /= 2.5f;
        if (Input.GetKey(KeyCode.C))
        {
            move /= 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            animManager.isCrouching = true;
            cam.gameObject.transform.Translate(0f, -0.5f, 0f);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            animManager.isCrouching = false;
            cam.gameObject.transform.Translate(0f, 0.5f, 0f);
        }
        if (Mathf.Abs(move.z) < 0.01 && Mathf.Abs(move.x) < 0.001)
        {
            animManager.speed = 0;
            gunPositionAnimator.SetInteger("Movement", 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animManager.speed = 2;
                gunPositionAnimator.SetInteger("Movement", 2);
            }
            else
            {
                animManager.speed = 1;
                gunPositionAnimator.SetInteger("Movement", 1);
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        controller.Move(move * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    [PunRPC]
    public void applyDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f)
            DIE();
    }

    [PunRPC]
    public void DIE()
    {
        Destroy(Instantiate(ragdollPrefab, transform.position, transform.rotation), 8f);
        PhotonNetwork.Destroy(gameObject);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
