using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Moves the player to the next scene and sends the gamemanager information about the player's current stats 
        if (collision.CompareTag("Player") || collision.CompareTag("Invulnerable"))
        {
            GameManager.instance.retainedHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth;
            GameManager.instance.retainedMagic = GameObject.Find("Player").GetComponent<PlayerMagic>().currentMagic;
            GameManager.instance.LoadNextScene();
        }
    }
}
