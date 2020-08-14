using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Modular so that all enemies can take damage.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        if (!anim.GetBool("isDead"))
        {
            StartCoroutine(Hurt());
        }
    }

    //Plays a death animation, allows the player to move past it, then is destroyed.
    void Die()
    {
        anim.SetTrigger("isDead");
        GetComponent<CircleCollider2D>().enabled = false;
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    //Adds a flash of white sprite rather than an animation to match current position.
    IEnumerator Hurt()
    {
        anim.enabled = false;
        sprite.sprite = hurtSprite;
        yield return new WaitForSeconds(.1f);
        anim.enabled = true;
    }

}
