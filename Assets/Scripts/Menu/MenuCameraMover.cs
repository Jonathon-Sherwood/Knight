using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMover : MonoBehaviour
{
    public float panSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (panSpeed * Time.deltaTime));
    }
}
