using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kills the player on contact, used at the bottom of the screen for falling death. 
        if ((collision.CompareTag("Player") || collision.CompareTag("Invulnerable")) && !GameObject.Find("Player").GetComponent<PlayerHealth>().died)
        {
            GameManager.instance.isDead = true;
            GameManager.instance.retainedHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().maxHealth;
            GameObject.Find("Player").GetComponent<PlayerController>().canJump = false;
            GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
            GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth = 0;
            GameManager.instance.retainedMagic = 0;
            AudioManager.instance.Play("Death");

            //turns off camera to imply player fell off screen.
            Destroy(GameObject.FindGameObjectWithTag("Cinemachine"));
        }
    }
}
