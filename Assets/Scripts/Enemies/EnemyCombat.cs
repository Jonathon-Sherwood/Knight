using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;

    [Range(0,5)] public int damage;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Tells the player which direction to flinch from and for how much damage.
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            playerHealth.TakeDamage(damage, direction);
        }
    }

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
