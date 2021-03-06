﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic script used on enemies for taking damage.
/// </summary>
public class EnemyTakeDamage : MonoBehaviour
{

    public int maxHealth; //Adjustable variable in the inspector for max health.
    int currentHealth; //Holds the amount of health the enemy currently has.
    public Sprite hurtSprite; //Place a white sillouhette in inspector for flashing effect.
    public GameObject deathPrefab; //Ensures the death animation plays where the object currently is.
    private Animator anim; //Calls this object's animator.
    private SpriteRenderer sprite; //Calls this object's sprite renderer.


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    //Recieves a damage value from the player and applies to this.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Dies if health is 0.
        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(Hurt());
    }

    //Plays a death animation, allows the player to move past it, then is destroyed.
    void Die()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        AudioManager.instance.Play("EnemyDie");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(transform.root.gameObject); //Destroys the entire gameobject related to this enemy rather than just the script holder.
    }

    //Adds a flash of white sprite rather than an animation to match current position.
    IEnumerator Hurt()
    {
        anim.enabled = false;
        AudioManager.instance.Play("EnemyHit");
        sprite.sprite = hurtSprite;
        yield return new WaitForSeconds(.1f);
        anim.enabled = true;
    }

}
