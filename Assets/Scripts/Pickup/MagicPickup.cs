using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPickup : MonoBehaviour
{
    PlayerMagic playerMagic; //Holds the player magic script.

    public float magicValue; //Adjustable amount of magic that is added.

    // Start is called before the first frame update
    void Start()
    {
        playerMagic = GameObject.Find("Player").GetComponent<PlayerMagic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Invulnerable"))
        {
            playerMagic.currentMagic += magicValue;
            Destroy(this.gameObject);
        }
    }
}
