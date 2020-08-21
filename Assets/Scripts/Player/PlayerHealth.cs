using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerController playerController; //Calls the player controller script to access Can Move.
    Rigidbody2D rb; //Calls the rigidbody on this object.
    SpriteRenderer sprite; //Calls the sprite renderer on this object.
    Animator anim; //Calls the animator on this object.
    PhysicsMaterial2D currentPhysics; //Holds onto the physics property already used.

    public Sprite deathSprite; //Replaces animator and sprite with a dead version.
    public Sprite hurtSprite; //Replaces animator and sprite with hurt version.
    public PhysicsMaterial2D frictionPhysics; //Swaps to a new physics material on stun.

    public float maxHealth; //Adjustable health for player.
    [HideInInspector] public float currentHealth; //Holds the current amount of health on player.
    [HideInInspector] public bool invulnerable; //Used for other scripts to detect if player is invulnerable.
    [HideInInspector] public bool died; //Used by other scripts to see if the player is dead.

    public float invulTimer; //Causes the player to be invulnerable after taking damage.
    public float stunTime; //Adjustable amount of time the player is stunned on hit.
    public float flashTime; //Adjustable amount of time the player flashes white on damage.

    public float deathKick; //Knocks the player backwards by a set amount

    float originalGravity;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = GameManager.instance.retainedHealth;
        originalGravity = rb.gravityScale;
        currentPhysics = rb.sharedMaterial;
    }

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //Called by enemy scripts to apply damage to player health
    public void TakeDamage(int damage, Vector2 direction)
    {
        if (currentHealth > 1)
        {
            AudioManager.instance.Play("PlayerHurt");
            rb.velocity = new Vector2(Mathf.Sign(direction.normalized.x) * deathKick * 0.2f, 5f);
            StartCoroutine(Stunned());
            StartCoroutine(Invulnerability());
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
        invulnerable = true;
        transform.gameObject.tag = "Invulnerable";
        gameObject.layer = 11;
        anim.enabled = false;
        sprite.sprite = hurtSprite;
        yield return new WaitForSeconds(flashTime);
        anim.enabled = true;
        sprite.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(invulTimer - flashTime);
        sprite.color = new Color(1f, 1f, 1f, 1f);
        transform.gameObject.tag = "Player";
        gameObject.layer = 10;
        invulnerable = false;
    }

    //Stops the player from moving after taking damage
    public IEnumerator Stunned()
    {
        playerController.canMove = false;           //Stops the player from moving
        rb.sharedMaterial = frictionPhysics;        //Changes the physics to high friction for no sliding.
        rb.gravityScale = 6;                        //Increases the gravity so there is no float.
        yield return new WaitForSeconds(stunTime);  //Keeps these changes for a set amount of time.
        rb.gravityScale = originalGravity;          //Returns each element to normal.
        rb.sharedMaterial = currentPhysics;
        playerController.canMove = true;
    }

    //Called on taking damage without enough health
    void Die(Vector2 direction)
    {
        invulnerable = true;
        GameManager.instance.isDead = true;
        died = true;
        //Removes animator so that sprite can be manually swapped, then knocks player back the direction they were hit from
        anim.enabled = false;
        rb.velocity = new Vector2(Mathf.Sign(direction.normalized.x) * deathKick, 10f);
        rb.drag = 5f;
        rb.gravityScale = 10;
        transform.Rotate(0f, 0f, 90f);
        gameObject.layer = 11; 
        GetComponent<SpriteRenderer>().sprite = deathSprite;
        playerController.canMove = false;
        playerController.canJump = false;
        AudioManager.instance.Play("Death");
        Destroy(GameObject.FindGameObjectWithTag("Cinemachine"));
        GameManager.instance.retainedHealth = maxHealth;
        GameManager.instance.retainedMagic = 0;
    }
}
