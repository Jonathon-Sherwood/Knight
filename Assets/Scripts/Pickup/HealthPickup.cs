using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerHealth playerHealth; //Calls the player health script.

    private void Start()
    {
        if (GameObject.Find("Player") == null)
        {
            return;
        }
        else
           playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Invulnerable"))
        {
            AudioManager.instance.Play("Health");
            playerHealth.currentHealth++;
            Destroy(this.gameObject);
        }
    }
}
