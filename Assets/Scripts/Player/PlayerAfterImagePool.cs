using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField] private GameObject afterImagePrefab; //Holds the image after image prefab.

    private Queue<GameObject> availableObjects = new Queue<GameObject>(); //Stores all objects that are not active.

    public static PlayerAfterImagePool instance { get; private set; } //Allows the after image script to call this.

    private void Awake()
    {
        instance = this; //Sets this to a singleton
        GrowPool(); //Adds at least one item to the pool to begin.
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab); //Allows compiler to make gameobject on load.
            instanceToAdd.transform.SetParent(transform); //Keeps the objects called organized within the hierarchy by childing.
            AddToPool(instanceToAdd); //Passes this instance into the function.
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false); //Begins the object as false.
        availableObjects.Enqueue(instance); //Adds each object to the end of the queue.
    }

    public GameObject GetFromPool()
    {
        if(availableObjects.Count == 0) //If there are no more objects in queue but still being called, more  will be made.
        {
            GrowPool();
        }

        var instance = availableObjects.Dequeue(); //Takes an object from the queue.
        instance.SetActive(true); //Turns on the gameobjects for the After Image script to use.
        return instance;
    }
}
