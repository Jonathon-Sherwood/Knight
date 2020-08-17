using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Allows this to be called by any script.

    //Sets this as a singleton.
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioManager.instance.Play("SplashScreen");
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            AudioManager.instance.Play("MenuMusic");
        }

        if (level == 2)
        {
            AudioManager.instance.Play("Level1Music");
        }
    }
}
