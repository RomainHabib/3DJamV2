using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
   // public GameManager gm;
   
    public float speed = 12f;

    public float gravity = -19.62f;
    Vector3 velocity;

    [Header("Ground Setup")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 3f;

    bool isGrounded;

    void Start()
    {
    }

    void Update()
    {
        Move();
        Crouch();
    }

    void Move()
    {
        //--- Reset de la vélocité ---//
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //--- Gère le mouvement du joueur sur X et Z ---//
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
      

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //--- Gère la gravité du joueur sur Y ---//
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    //--- Gère l'accroupissement du joueur ---//
    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 0.35f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
