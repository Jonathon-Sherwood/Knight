using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float holdTime = 5f; //Adjustable amount of time for the designer in the inspector.

    void Start()
    {
        //Sets the timer to be above current time by variable amount.
        holdTime = Time.time + holdTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Once time has gone longer than the variable, load the start screen.
        if (holdTime <= Time.time)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
