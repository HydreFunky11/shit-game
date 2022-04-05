using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{

    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 300f;
    public bool isSprinting =false;
    public float sprintingMultiplier;

    public bool isCroushing = false;
    public float croushMultiplier;
    public float croushingHeight = 2.5f;
    public float basicHeight = 3.8f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
            speed = 12f;
        }

        if (isSprinting == true && speed <= 24)
        {
            speed *= sprintingMultiplier;
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCroushing = true;
        }
        else
        {
            isCroushing = false;
        }

        if (isCroushing == true)
        {
            controller.height = croushingHeight;
            speed *= croushMultiplier;
        }
        else
        {
            controller.height = basicHeight;
        }
    }
}
