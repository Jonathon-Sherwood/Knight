using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; //Adjustable player speed in inspector.
    public float jumpForce; //Adjustable jump height in inspector.

    private Rigidbody2D rb; //Calls the Rigidbody on the player gameobject.
    private BoxCollider2D boxCollider; //Calls the boxcollider on the player gameobject.
    private SpriteRenderer sprite; //Calls the sprite renderer on the player gameobject.
    private Animator anim; //Calls the animator on the player gameobject.
    private Dash dash; //Allows the script to know if the player is dashing.
    

    public float hangTime = 0.2f; //Changes how long the player can jump after walking off platform.
    private float hangCounter; //Holds how long the player has been off of a platform.

    public int extraJumps; //Adjustbale amount of multi-jumps in inspector.
    private int currentExtraJumps; //Holds the current value of extra jumps used.

    private bool hasJumped = false; //Used to check if player jumped or just fell.

    [HideInInspector]public bool flipX; //Holds whether or not the player is already flipped on the x axis.

    [HideInInspector]public bool canMove = true; //Holds a value of the player being able to move.

    public LayerMask groundLayerMask; //Allows the IsGrounded raycast to only hit the floor.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        dash = gameObject.GetComponent<Dash>();
        currentExtraJumps = extraJumps;
    }

    private void Update()
    {
        Jump();
        AnimationControl();
        if (canMove) Movement();
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
        if (currentExtraJumps >= 0 && Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasJumped = true;
            currentExtraJumps--;
            anim.Play("Player_Jump");
        }

        //If the player hasn't jumped but is falling, they lose an extra jump.
        if(hangCounter <= 0f && hasJumped == false)
        {
            hasJumped = true;
            currentExtraJumps--;
        }

        //Adjustable jump height based on length of spacebar pressed.
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            hangCounter = 0;
        }

        //Used to hold a brief timer between player leaving the ground and being able to jump.
        if (IsGrounded())
        {
            hangCounter = hangTime;
            if (rb.velocity.y > -.13f && rb.velocity.y < .13f) //Ensures that variables don't reset the same frame the player leaves the ground.
            {
            currentExtraJumps = extraJumps;
            hasJumped = false;
                anim.SetBool("Grounded", true);
            }
        }
        else if (!IsGrounded() && currentExtraJumps == extraJumps)
        {
            hangCounter -= Time.deltaTime;
            anim.SetBool("Grounded", false);
        }
    }

    private void AnimationControl()
    {
        if((rb.velocity.x > .01 || rb.velocity.x < -.01) && canMove)
        {
            anim.SetBool("Running", true);
        } else
        {
            anim.SetBool("Running", false);
        }

        if (dash.isDashing)
        {
            anim.Play("Player_Dash");
        }

        if (IsGrounded() && rb.velocity.y > -.13f && rb.velocity.y < .13f) //Ensures that variables don't reset the same frame the player leaves the ground.
        {
            anim.SetBool("Grounded", true);
        } else
        {
            anim.SetBool("Grounded", false);
        }

        //Placeholders to test animations
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.Play("Player_Magic");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.Play("Player_Attack");
        }

        if (Input.GetKey(KeyCode.Mouse3))
        {
            anim.Play("Player_Death");
            rb.constraints = RigidbodyConstraints2D.None;
            rb.gravityScale = 1;
        }

    }


    //Checks to see if the player is touching the floor.
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .2f, groundLayerMask);
        return raycastHit.collider != null;
    }
}