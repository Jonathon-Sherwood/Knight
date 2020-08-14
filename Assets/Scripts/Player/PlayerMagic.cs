using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [HideInInspector] public float currentMagic; //Holds the value of magic currently accrued.

    public float magicSpeed;
    public int magicDamage;

    public float magicMax; //Sets the most magic the player can have.
    public float magicCost; //Set cost of magic when shot.

    public GameObject magicShotPrefab; //Assigns the prefab that will be shot.
    Transform shootPoint;

    private void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;
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

    void Magic()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && currentMagic >= 10)
        {
            currentMagic -= magicCost;
            GetComponent<Animator>().Play("Player_Magic");
            Instantiate(magicShotPrefab, shootPoint.position, shootPoint.rotation);
        } else if (Input.GetKeyDown(KeyCode.Mouse1) && currentMagic <= 0)
        {
            GetComponent<Animator>().Play("Player_Magic");
        }
    }
}
