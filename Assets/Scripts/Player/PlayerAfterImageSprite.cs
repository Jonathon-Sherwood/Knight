using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private float activeTime = 0.1f; //Holds how long the object will be active.
    private float timeActivated; //Holds how long the object has been active.
    private float alpha; //Holds what our alpha currently is.
    private float alphaSet = 0.8f; //Allows changing the alpha of the sprite.
    private float alphaMultiplier = 0.85f; //Changes the sprite from solid to faded.

    private Transform player; //Holds the location of the player object.
    private SpriteRenderer sprite; //Holds this gameobjects sprite.
    private SpriteRenderer playerSprite; //Holds the player's sprite.
    private Color color; //Holds our current color;


    //Sets all variables to be equal to the player when enabled.
    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        sprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        //Decreases the alpha by a fixed rate.
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        sprite.color = color;

        if(Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.instance.AddToPool(gameObject); //Adds this gameobject to the Pool script's queue on creation.

        }
    }
}
