using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    public static GameManager instance; //Allows all scripts to call this.
    public int currentSceneIndex = 0; //Holds the number for whichever scene we are on.
    [HideInInspector] public int playerLives; //Holds onto the value of the players current lives;
    public int MaxPlayerLives; //Allows the designer to choose how many times the player can die.
    public float deathDelay; //Allows the designer to change how long the scene persists after death.

    [HideInInspector] public float retainedHealth;
    [HideInInspector] public float retainedMagic;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        //Turns the gamemanager into a singleton.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        playerLives = MaxPlayerLives;
    }

    private void Start()
    {
        AudioManager.instance.Play("SplashScreen"); //Starts playing music on start.
        retainedHealth = 5;
    }

    private void Update()
    {
        if (isDead)
        {
            StartCoroutine(DeathDelay());
        }
    }

    //Used to watch the dead player before respawning or ending.
    IEnumerator DeathDelay()
    {
        AudioManager.instance.Stop("Level1Music");
        isDead = false;
        yield return new WaitForSeconds(deathDelay);
        //Switches to the lose screen on player death 4 times, otherwise respawns player.
        if ((currentSceneIndex != (4) && currentSceneIndex != (0) && currentSceneIndex != (3)) && playerLives < 0)
        {
            LoadLevel(4);
        }
        else if ((currentSceneIndex != (4) && currentSceneIndex != (0) && playerLives >= 0))
        {
            playerLives--;
            LoadLevel(currentSceneIndex);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// This method is called every time a scene finishes loading.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneIndex = scene.buildIndex;

        if (currentSceneIndex == 1)
        {
            AudioManager.instance.Play("MenuMusic");
        }
        if (currentSceneIndex == 2)
        {
            AudioManager.instance.Play("Level1Music");
        }
    }

    public void LoadNextScene()
    {
        LoadLevel(currentSceneIndex + 1);
    }


}

