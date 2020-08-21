using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic script used on all enemies to deal damage on touching.
/// </summary>
public class EnemyCombat : MonoBehaviour
{
    GameObject player; //Holds the value of the player in game
    PlayerHealth playerHealth; //Holds the value of the player's health in game

    [Range(0,5)] public int damage; //Adjustable amount of damage limited to max player health

    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    //Used for the first time the enemy touches the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Tells the player which direction to flinch from and for how much damage.
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            playerHealth.TakeDamage(damage, direction);
        }
    }

    //Used to continue hurting the player if they stay in contact with enemy
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Tells the player which direction to flinch from and for how much damage.
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            playerHealth.TakeDamage(damage, direction);
        }
    }
}
