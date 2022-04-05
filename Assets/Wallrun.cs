using UnityEngine;
using System;

public class WallRun : MonoBehaviour
{
    //Wallrun
    public LayerMask whatIsWall;
    public float wallRunForce, maxWallRunTime, maxWallSpeed;
    bool isWallright, isWallLeft;
     bool isWallRunning;
     public float maxWallRunCameraTilt, wallRunCameraTilt;

     private void WallRunInput()
     {
         //start Wallrun
        if (WallRunInput.GetKey(keycode.D) && isWallRight) StartWAllRun();
        if (WallRunInput.GetKey(keycode.Q) && isWallLeft) StartWAllRun();
     }
     private void StartWallRun()
     {
         rb.useGravity = false;
         isWallRunning = true;

         if (rb.velocity.magnetude <= maxWallSpeed)
         {
             rb.addForce(orientation.forward * wallRunForce * Time.deltaTime);
         
        if (isWallright)
        {
            rb.addForce(orientation.right * wallRunForce / 5 * Time.deltaTime);
        }
        else
        {
            rb.addForce(-orientation.right * wallRunForce / 5 * Time.deltaTime);
        }
        }

     }
    private void StopWallrun()
    {
        rb.useGravity = true;
        isWallRunning = false;
    }
    private void CheckForWall()
    {
        isWallright = Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);
        isWallleft = Physics.Raycast(transform.position, -orientation.left, 1f, whatIsWall);
    
        //leave wall run
        if (!isWallLeft && !isWallRight) StopWallrun();
        //reset double jump
        if(isWallLeft || isWallRight) doubleJumpsLeft = startDoubleJumps;
    }



    //assignables
    public Transform playerCam;
    public Transform orientation;

    //autres
    private RigiBody rb;

    //rotation and look
    private float xRotation;
    private float sensibility = 50f;
    private float sensMultiplier = 1f;

    //mouvement
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    private float startMaxSpeed;
    public bool grounded;
    public LayerMask whatisGround;





























private void Jump()
{
    if(grounded)
    {
        readyToJump = false;

        //forces de jump
        rb.AddForce(Vector2.up * jumpForce * 1.5f);
        rb.AddForce(normalVector * jumpForce * 0.5f);

        //si saut quand tu tombes, stopper le perso
        Vector3 vel = rb.velocity;
        if (rb.velocity.y <0.5f)
        {
            rb.velocity = new Vector3(vel.x, 0, vel.z);
        }
        else if (rb.velocity.y >0)
        {
            rb.velocity = new Vector3(vel.x,vel.y / 2, vel.z);
            invoke(nemeof(resetJump), jumpCooldown);
        } 
    }



    if (!rounded)
    {
        readyToJump =false;

        //ajout force de jump
        rb.AddForce(orientation.forward * jumpForce * 1f);
        rb.AddForce(Vector2.up * jumpForce *1.5f);
        rb.Addforce(normalVector * jumpForce * 0.5f);

        //reset velocity
        rb.Velocity = Vector3.zero;

        //disable dashforcecounter if doublejumping while dashing
        allowDashForceCounter = false;

        Invoke(nameof(ResetJump),jumpCooldown);
    }





    Walljump
        if (isWallRunning)
        {
            readyToJump = false;

            //normaljump
            if (isWallLeft && !WallRunInput.GetKey(KeyCode.D) || isWallright && !WallRunInput.GetKey(KeyCode.Q))
            {
                rb.AddForce(Vector2.up * jumpForce * 1.5f);
                rb.AddForce(normalVector  * jumpForce * 0.5f);
            }
            
            //sidewards wallhop
            if (isWallRight || isWallLeft && WallRunInput(KeyCode.Q) || WallRunInput(KeyCode.D))rb.AddForce(-orientation.up * jumpForce * 3.2f);
            if (isWallRight && WallRunInput(KeyCode.D))rb.AddForce(-orientation.up * jumpForce * 3.2f);
            if (isWalllEFT && WallRunInput(KeyCode.Q))rb.AddForce(orientation.up * jumpForce * 3.2f);
        

            //always add forward force
            rb.AddForce(orientation.forward * JumpForce * 1f);

            //reset velocity
            rb.AddForce = Vector3.zero;

            //disable dashforcecounter if doublejumping while dashing
            allowDashForceCounter = false;

            Invoke(nameof(ResetJump), jumpCooldown);
        }
}

private void ResetJump()
{
    readyToJump  = true;
}



    private void Update()
        {
            MyInput();
            Look();
            CheckForWall();
            SonicSpeed();
            WallRunInput();
        }
}