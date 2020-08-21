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
    }

    private void Start()
    {
        AudioManager.instance.Play("SplashScreen"); //Starts playing music on start.
        playerLives = MaxPlayerLives;
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
        playerLives--;
        isDead = false;
        yield return new WaitForSeconds(deathDelay);
        //Switches to the lose screen on player max death times, otherwise respawns player.
        if ((currentSceneIndex != (5) && currentSceneIndex != (0) && playerLives <= 0))
        {
            LoadLevel(6);
        }
        else if ((currentSceneIndex != (5) && currentSceneIndex != (0) && playerLives > 0))
        {
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
            AudioManager.instance.Stop("LoseScreen");
            AudioManager.instance.Stop("WinScreen");
            playerLives = MaxPlayerLives;
            retainedMagic = 0;
        }
        if (currentSceneIndex == 2)
        {
            AudioManager.instance.Play("Level1Music");
            retainedHealth = GameObject.Find("Player").GetComponent<PlayerHealth>().maxHealth;

        }
        if (currentSceneIndex == 5)
        {
            AudioManager.instance.Stop("Level1Music");
            AudioManager.instance.Play("WinScreen");
        }
        if (currentSceneIndex == 6)
        {
            AudioManager.instance.Stop("Level1Music");
            print("Test");
            AudioManager.instance.Play("LoseScreen");
        }
    }

    public void LoadNextScene()
    {
        LoadLevel(currentSceneIndex + 1);
    }


}

