using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject quitMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject playerDummy;

    public float startDelay;
    public float playerDummyRunSpeed;

    private bool gameStarted = false;

    private void Update()
    {
        if (gameStarted)
        {
            playerDummy.transform.Translate(Vector3.right * (playerDummyRunSpeed * Time.deltaTime));
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameStarted)
        {
            Cancel();
        }
    }

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

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene(2);
    }

    public void CreditsMenu()
    {
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void QuitMenu()
    {
        optionsMenu.SetActive(false);
        quitMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Cancel()
    {
        optionsMenu.SetActive(true);
        quitMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
}
