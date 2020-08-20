using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public CircleCollider2D detectionRange; //Attaches to the child called detection range
    public CircleCollider2D chaseRange; //Attaches to the child called chase range

    public float movementSpeed;
    public float attackReflect;
    public float reflectTime;
    float reflectedTime;

    private GameObject player; //Used to find the player
    private Animator anim; //Used to call the animator

    private bool chasing;
    private bool touch; //Only reflects if touching player.

    Vector3 targetPosition;
    Vector3 directionToLook;

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
        if (!player.GetComponent<PlayerHealth>().invulnerable && !touch)
        {
            transform.position += directionToLook.normalized * movementSpeed * Time.deltaTime;      //Moves the bat towards the player.
            targetPosition = player.transform.position;
            directionToLook = targetPosition - transform.position;
            reflectedTime = reflectTime;
        } else if (player.GetComponent<PlayerHealth>().invulnerable && touch)
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
        if (detectionRange.IsTouchingLayers(LayerMask.GetMask("Player")) || detectionRange.IsTouchingLayers(LayerMask.GetMask("Invulnerable")))
        {
            anim.SetTrigger("Detected");
        }

        if (chaseRange.IsTouchingLayers(LayerMask.GetMask("Player")) || detectionRange.IsTouchingLayers(LayerMask.GetMask("Invulnerable")))
        {
            anim.SetTrigger("Chasing");
            chaseRange.radius = 9;  //Increases chasing size once they have detected the player
            chasing = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        chasing = false;
    }
}
