﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; //Adjustable player speed in inspector.
    public float jumpForce; //Adjustable jump height in inspector.

    private Rigidbody2D rb; //Calls the Rigidbody on the player gameobject.
    private BoxCollider2D boxCollider; //Calls the boxcollider on the player gameobject.
    private SpriteRenderer sprite; //Calls the sprite renderer on the player gameobject.
    

    public float hangTime = 0.2f; //Changes how long the player can jump after walking off platform.
    private float hangCounter; //Holds how long the player has been off of a platform.

    public float jumpBufferLength = 0.1f; //Adds a slight distance the player can press jump before hitting the ground where it still registers.
    private float jumpBufferCount; //Holds the value of the jump buffer length.

    public int extraJumps; //Adjustbale amount of multi-jumps in inspector.
    private int currentExtraJumps; //Holds the current value of extra jumps used.

    [HideInInspector]public bool flipX; //Holds whether or not the player is already flipped on the x axis.

    [HideInInspector]public bool canMove = true; //Holds a value of the player being able to move.

    public LayerMask groundLayerMask; //Allows the IsGrounded raycast to only hit the floor.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentExtraJumps = extraJumps;
    }

    private void Update()
    {
        Jump();
        if (canMove) Movement();

        //Used to hold a brief timer between player leaving the ground and being able to jump.
        if (IsGrounded())
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        //Adds a buffer to when the player presses the jump button just before hitting the floor.
        if (Input.GetKeyDown(KeyCode.Space) && currentExtraJumps > 0)
        {
            jumpBufferCount = jumpBufferLength;
            currentExtraJumps--;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        if (IsGrounded())
        {
            currentExtraJumps = extraJumps; //Resets extra jumps on contact with ground.
        }
    }

    private void Movement()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;  //Sets a float to the value added by Unity's input for horizontal.

        rb.velocity = new Vector2(xMovement, rb.velocity.y);  //The velocity is always tied to the input from Unity.

        SpriteFlip(xMovement);  //Sets the sprite flip method to whatever the current axis is.
    }

    //Sprite direction based on movement using FlipX component
    private void SpriteFlip(float xMovement)
    {
        if (xMovement < -0.1f && !flipX)
        {
            transform.Rotate(0f, 180f, 0f);
            flipX = true;
        }

        if (xMovement > 0.1f && flipX)
        {
            transform.Rotate(0f, 180f, 0f);
            flipX = false;
        }
    }

    private void Jump()
    {
        //Jumping, giving the player a buffer before being unable to jump.
        if (jumpBufferCount >= 0 && hangCounter > 0f && currentExtraJumps == 0) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (currentExtraJumps > 0 && Input.GetKeyDown(KeyCode.Space)) //Allows the player to extra jump regardless of being near the floor.
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

            //Adjustable jump height based on length of spacebar pressed.
            if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpBufferCount = 0;
                hangCounter = 0;
            }
    }


    //Checks to see if the player is touching the floor.
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayerMask);
        return raycastHit.collider != null;
    }
}