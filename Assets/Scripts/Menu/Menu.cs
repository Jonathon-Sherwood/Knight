using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject quitMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Startgame()
    {
        SceneManager.LoadScene(2);
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
