using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb; //Holds the rigid body.
    [HideInInspector] public bool isDashing; // Used to enable methods based on dashing.
    private float dashTimeLeft; //Holds the current time of dash cooldown.
    private float lastImageXpos; //Tracks the x coordinate of last placed after image.
    private float lastDash = -100; //Used to check for cooldown based on last time dash used.
    private PlayerController playerController; //Holds the player controller script.

    public float dashTime; //Adjustable amount of time for player dashing in inspector.
    public float dashSpeed; //Adjustale amount of speed for player dashing in inspector.
    public float distanceBetweenImages; //Adjustable distance between afterimages in inspector.
    public float dashCooldown; //Adjustale amount of time between player dashes in inspector.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDash();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(Time.time >= (lastDash + dashCooldown))
            AttemptToDash();
        }
    }

    //This consistently checks to see if dashing is available, and pushes the player forward if so.
    private void CheckDash()
    {
        if (isDashing)
        {
            //Pushes player forward of facing direction and locks movement
            if (dashTimeLeft > 0)
            {
                playerController.canMove = false;
                if (playerController.flipX)
                {
                    rb.velocity = new Vector2(dashSpeed * -1, 0);
                }
                else
                {
                    rb.velocity = new Vector2(dashSpeed * 1, 0);
                }
                dashTimeLeft -= Time.deltaTime;

                //Sets each after image location within a set distance.
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            //Returns control to player and stops dashing.
            if(dashTimeLeft <= 0)
            {
                playerController.canMove = true;
                isDashing = false;
            }
        }
    }

    //This will allow the checkdash method to reset variables and allow for dashing
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }
}
