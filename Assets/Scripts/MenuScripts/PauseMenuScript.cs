using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject PauseaGameMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ContinueGame();
            else
                PauseGame();
        }
    }

    public void ContinueGame()
    {
        PauseaGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        PauseaGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("The game is Exiting now!");
        Application.Quit();
    }
}
