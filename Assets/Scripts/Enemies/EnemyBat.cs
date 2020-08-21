using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public CircleCollider2D detectionRange; //Attaches to the child called detection range
    public CircleCollider2D chaseRange; //Attaches to the child called chase range

    public float movementSpeed; //Adjustable movement speed in inspector
    public float attackReflect; //Adjustable speed of reflection in inspector
    public float reflectTime; //Adjustable reflection time in inspector
    float reflectedTime; //Holds the current value of time being reflected

    private GameObject player; //Used to find the player
    private Animator anim; //Used to call the animator

    private bool chasing;
    private bool touch; //Only reflects if touching player.

    Vector3 targetPosition; //Used to detect the player
    Vector3 directionToLook; //Used to look at the player

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        reflectedTime = reflectTime;
    }

    private void Update()
    {
        if(chasing) Movement();
    }

    void Movement()
    {
        //Chases the player so long as the player is vulnerable
        if (!player.GetComponent<PlayerHealth>().invulnerable && !touch)
        {
            transform.position += directionToLook.normalized * movementSpeed * Time.deltaTime;     
            targetPosition = player.transform.position;
            directionToLook = targetPosition - transform.position;
            reflectedTime = reflectTime;
        }
        //Reflects away from the player and waits until the player is vulnerable again on successful hit
        else if (player.GetComponent<PlayerHealth>().invulnerable && touch)
        {
            reflectedTime--;
            if (reflectedTime >= 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + ( attackReflect), transform.position.z);
            } else
            {
                touch = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Shows the player that the bat has recognized them if within range
        if (detectionRange.IsTouchingLayers(LayerMask.GetMask("Player")) || detectionRange.IsTouchingLayers(LayerMask.GetMask("Invulnerable")))
        {
            anim.SetBool("Detected", true);
        }

        //Chases player if within range
        if (chaseRange.IsTouchingLayers(LayerMask.GetMask("Player")) || detectionRange.IsTouchingLayers(LayerMask.GetMask("Invulnerable")))
        {
            anim.SetTrigger("Chasing");
            chaseRange.radius = 9;  //Increases chasing size once they have detected the player
            chasing = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Used to reflect away after touching
        if (collision.gameObject.CompareTag("Player"))
        {
            touch = true;
        }
    }

    //Stops the bat from chasing after certain distance
    private void OnTriggerExit2D(Collider2D collision)
    {
        chasing = false;
        anim.SetBool("Detected", false);
    }
}
