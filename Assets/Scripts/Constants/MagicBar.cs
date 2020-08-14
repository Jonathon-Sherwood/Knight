using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{
    PlayerMagic playerMagic; //Calls the player magic script.
    Slider slider; //Calls the slider.

    public Image borderImage; //Allows the designer to set the bar as the image;

    public Sprite filledBorder; //Uses the border that has the circle filled in.
    public Sprite emptyBorder; //Removes tip of bar fill on empty.

    private void Start()
    {
        playerMagic = GameObject.Find("Player").GetComponent<PlayerMagic>();
        slider = GetComponent<Slider>();
        slider.maxValue = playerMagic.magicMax;
        borderImage.sprite = emptyBorder;
    }


    private void Update()
    {
        slider.value = playerMagic.currentMagic;

        //Due to the border starting filled, change the image based on fill condition.
        if(playerMagic.currentMagic == 0)
        {
            borderImage.sprite = emptyBorder;
        } else
        {
            borderImage.sprite = filledBorder;
        }
    }
}
