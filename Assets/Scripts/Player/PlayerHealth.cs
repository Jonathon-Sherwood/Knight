﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController playerController; //Calls the player controller script to access Can Move.
    Rigidbody2D rb; //Calls the rigidbody on this object.
    SpriteRenderer sprite; //Calls the sprite renderer on this object.
    Animator anim; //Calls the animator on this object.

    public Sprite deathSprite; //Replaces animator and sprite with a dead version.

    public float maxHealth; //Adjustable health for player.
    float currentHealth; //Holds the current amount of health on player.

    public float invulTimer; //Causes the player to be invulnerable after taking damage.
    public float stunTime; //Adjustable amount of time the player is stunned on hit.

    public float deathKick; //Knocks the player backwards by a set amount

    float originalGravity;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        originalGravity = rb.gravityScale;
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (currentHealth > 20)
        {
            rb.velocity = new Vector2(Mathf.Sign(direction.normalized.x) * deathKick * 0.2f, 5f);
            StartCoroutine(Stunned());
            StartCoroutine(Invulnerability());
            //AudioManager.instance.Play("PlayerHit");
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(direction);
        }
    }

    //used to keep player invulnerable for the brief period after taking damage
    public IEnumerator Invulnerability()
    {
        transform.gameObject.tag = "Invulnerable";
        sprite.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(invulTimer);
        sprite.color = new Color(1f, 1f, 1f, 1f);
        transform.gameObject.tag = "Player";
    }

    public IEnumerator Stunned()
    {
        playerController.canMove = false;
        rb.angularDrag = 100;
        rb.gravityScale = 10;
        yield return new WaitForSeconds(stunTime);
        rb.gravityScale = originalGravity;
        rb.angularDrag = 0f;
        playerController.canMove = true;
    }

    void Die(Vector2 direction)
    {
        //Removes animator so that sprite can be manually swapped, then knocks player back the direction they were hit from
        anim.enabled = false;
        //AudioManager.instance.Play("PlayerDeath");
        rb.velocity = new Vector2(Mathf.Sign(direction.normalized.x) * deathKick, 10f);
        rb.drag = 5f;
        rb.gravityScale = 10;
        transform.Rotate(0f, 0f, 90f);
        //boxCollider.size = new Vector2(.75f, boxCollider.size.y);
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        playerController.canMove = false;
    }
}
