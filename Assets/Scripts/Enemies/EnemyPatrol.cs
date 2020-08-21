using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attachable to any enemy that needs to patrol. Put this on a parent of the actual enemy so it can flipx without reversing movement.
/// </summary>
public class EnemyPatrol : MonoBehaviour
{

    public float moveSpeed; //Adjustable movement speed in the inspector
    public float pauseTime; //Adjustable amount of time stopped at each patrol point
    public Rigidbody2D rb; //Calls the rigidbody throughout the script
    public GameObject body; //The body should be the child of this object so it can move freely between points
    public Transform[] points; //An array of points the enemy can patrol between
    private Transform currentPoint; //Holds the location of the current patrol point
    private int pointSelection; //Holds the value of the current patrol point
    private bool canMove = true; //Allows the coroutine to pause platforms.
    private bool flipX; //Flips the enemy on their X axis depending on direction moved


    private void Start()
    {
        currentPoint = points[pointSelection];
    }

    private void Update()
    {
        Movement();
        SpriteFlip();
    }

    //Used for adding a delay between patrol points.
    IEnumerator PauseTime()
    {
        canMove = false;
        yield return new WaitForSeconds(pauseTime);
        canMove = true;
    }

    private void Movement()
    {
        //Moves towards current point in the array
        if (canMove)
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        //Once the enemy reaches its destination, it starts the pausetime and picks the next location
        if (transform.position == currentPoint.position)
        {
            StartCoroutine(PauseTime());
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }
            currentPoint = points[pointSelection];
        }
    }

    //Sprite direction based on movement using FlipX component
    private void SpriteFlip()
    {
        float direction = (currentPoint.position.x - transform.position.x);

        if (direction > 0)
        {
            body.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        } else
        {
            body.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
    }

}
