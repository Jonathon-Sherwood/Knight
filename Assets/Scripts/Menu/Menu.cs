using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    //Holds onto the various menus that need to be turned on and off
    public GameObject quitMenu; 
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    //Holds onto the player animation in the opening
    public GameObject playerDummy;

    public float startDelay; //time before the first level loads
    public float playerDummyRunSpeed; //speed of player running off camera on start

    private bool gameStarted = false; //Starts the dummy running on play

    private void Update()
    {
        //Runs the player dummy off camera on start of game
        if (gameStarted)
        {
            playerDummy.transform.Translate(Vector3.right * (playerDummyRunSpeed * Time.deltaTime));
        }

        //Always takes the player back to the main options menu on pressing escape.
        if (Input.GetKeyDown(KeyCode.Escape) && !gameStarted)
        {
            Cancel();
        }
    }

    //Deactivates player interaction and begins opening sequence.
    public void Startgame()
    {
        AudioManager.instance.Play("StartClick");
        AudioManager.instance.Stop("MenuMusic");
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);
        creditsMenu.SetActive(false);
        gameStarted = true;
        StartCoroutine(StartDelay());
    }

    //Pauses from when the player presses play to when the next scene loads.
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene(2);
    }

    //Placed on button to open main menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }

    //Deactivates other menus and opens credits
    public void CreditsMenu()
    {
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    //Deactivates other menus and opens the quit menu
    public void QuitMenu()
    {
        optionsMenu.SetActive(false);
        quitMenu.SetActive(true);
    }

    //If you're in the quit menu and press "yes" exit game.
    public void Quit()
    {
        Application.Quit();
    }

    //Returns to options menu
    public void Cancel()
    {
        optionsMenu.SetActive(true);
        quitMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
}
