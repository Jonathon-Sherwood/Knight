using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructTimer : MonoBehaviour
{
    /// <summary>
    /// Used for removing gameobjects from the world.
    /// </summary>
    public float destructTimer;

    // Start is called before the first frame update
    void Start()
    {
        destructTimer = Time.time + destructTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > destructTimer)
        {
            Destroy(this.gameObject);
        }
    }
}
