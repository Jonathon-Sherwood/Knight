using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [HideInInspector] public float currentMagic; //Holds the value of magic currently accrued.

    // Update is called once per frame
    void Update()
    {
        Magic();
    }

    void Magic()
    {
        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            //shoot magic.
        }
    }
}
