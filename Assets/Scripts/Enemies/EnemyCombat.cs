using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;

    public int damage;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            playerHealth.TakeDamage(damage, direction);
        }
    }
}
