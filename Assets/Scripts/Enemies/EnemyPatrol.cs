using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float moveSpeed;
    public float pauseTime;
    public Rigidbody2D rb;
    public GameObject body;
    public Transform[] points;
    private Transform currentPoint;
    private int pointSelection;
    private bool canMove = true; //Allows the coroutine to pause platforms.
    private bool flipX;


    private void Start()
    {
        currentPoint = points[pointSelection];
    }

    private void Update()
    {
        Movement();
        SpriteFlip();
    }

    IEnumerator PauseTime()
    {
        canMove = false;
        yield return new WaitForSeconds(pauseTime);
        canMove = true;
    }

    private void Movement()
    {
        if (canMove)
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        //Once 
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
