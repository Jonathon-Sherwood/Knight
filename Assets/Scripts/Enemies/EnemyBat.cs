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

    Vector3 targetPosition;
    Vector3 directionToLook;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        reflectedTime = reflectTime;
    }


    void Movement()
    {
        print(reflectedTime);
        if (!player.GetComponent<PlayerHealth>().invulnerable)
        {
            transform.position += directionToLook.normalized * movementSpeed * Time.deltaTime;      //Moves the ship towards the player.
            targetPosition = player.transform.position;
            directionToLook = targetPosition - transform.position;
            reflectedTime = reflectTime;
        } else
        {
            reflectedTime--;
            if (reflectedTime >= 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + (movementSpeed * attackReflect), transform.position.z);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (detectionRange.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            anim.SetTrigger("Detected");
        }

        if (chaseRange.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            anim.SetTrigger("Chasing");
            Movement();
        }
    }
}
