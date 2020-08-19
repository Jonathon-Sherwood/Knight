using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kills the player on contact, used at the bottom of the screen for falling death. 
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.retainedHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth;
            GameManager.instance.retainedMagic = GameObject.Find("Player").GetComponent<PlayerMagic>().currentMagic;
            GameManager.instance.LoadNextScene();
        }
    }
}
