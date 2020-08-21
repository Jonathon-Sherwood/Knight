using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsTracker : MonoBehaviour
{
    PlayerHealth playerHealth; //Calls the player health script for tracking hearts.

    public Sprite heartFull; //The red heart that indicates the player has that health
    public Sprite heartEmpty; //The grey heart that indicates the player lost that health

    //Each is attached to a different heart in the UI
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;

    // Start is called before the first frame update
    private void Start()
    {
        if(GameObject.Find("Player")== null)
        {
            return;
        } else
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }


    private void Update()
    {
        //Swaps sprites based on missing health. For each lost, another heart is swapped to an empty sprite.
        if(playerHealth.currentHealth == 5)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartFull;
            heart4.sprite = heartFull;
            heart5.sprite = heartFull;
        } else if (playerHealth.currentHealth == 4)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartFull;
            heart4.sprite = heartFull;
            heart5.sprite = heartEmpty;
        } else if (playerHealth.currentHealth == 3)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartFull;
            heart4.sprite = heartEmpty;
            heart5.sprite = heartEmpty;
        } else if (playerHealth.currentHealth == 2)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartEmpty;
            heart4.sprite = heartEmpty;
            heart5.sprite = heartEmpty;
        } else if (playerHealth.currentHealth == 1)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            heart4.sprite = heartEmpty;
            heart5.sprite = heartEmpty;
        } else if (playerHealth.currentHealth == 0)
        {
            heart1.sprite = heartEmpty;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            heart4.sprite = heartEmpty;
            heart5.sprite = heartEmpty;
        }
    }
}
