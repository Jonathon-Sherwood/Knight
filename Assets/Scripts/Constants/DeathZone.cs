using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kills the player on contact, used at the bottom of the screen for falling death. 
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.isDead = true;

            //turns off camera to imply player fell off screen.
            Destroy(GameObject.FindGameObjectWithTag("Cinemachine"));
        }
    }
}
