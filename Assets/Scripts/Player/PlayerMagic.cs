using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [HideInInspector] public float currentMagic; //Holds the value of magic currently accrued.

    public float magicSpeed; //How fast the magic bullet flies
    public int magicDamage; //How much damage the magic bullet deals

    public float castRate; //How frequent the magic can be cast
    float nextCastTime; //Holds the value of current casting delay

    public float magicMax; //Sets the most magic the player can have.
    public float magicCost; //Set cost of magic when shot.
    public float magicShotDestructionTime; //Allows the designer to destroy the bullet early.

    public GameObject magicShotPrefab; //Assigns the prefab that will be shot.
    Transform shootPoint;

    private void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;
        currentMagic = GameManager.instance.retainedMagic;
    }

    // Update is called once per frame
    void Update()
    {
        Magic();

        //Stops the player from having more than the max.
        if(currentMagic > magicMax)
        {
            currentMagic = magicMax;
        }

        //Stops the player's magic from being negative.
        if(currentMagic < 0)
        {
            currentMagic = 0;
        }
    }


    //Instantiates a magic shot that flies towards facing direction and deals damage. The bullet has its own script
    void Magic()
    {

        if (Time.time >= nextCastTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && currentMagic >= 10)
            {
                currentMagic -= magicCost;
                GetComponent<Animator>().Play("Player_Magic");
                Instantiate(magicShotPrefab, shootPoint.position, shootPoint.rotation);
                nextCastTime = Time.time + 1f / castRate;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && currentMagic <= 0)
            {
                GetComponent<Animator>().Play("Player_Magic");
                nextCastTime = Time.time + 1f / castRate;
            }
        }
    }
}
