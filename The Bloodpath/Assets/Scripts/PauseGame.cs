using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseGame : MonoBehaviour
{
    //private GameObject pauseMenPreFab;
    private GameObject pauseMenu;
    public GameObject pauseObject;

    void Awake()
    {
        Time.timeScale = 0;
        pauseMenu = Resources.Load("PreFabs/PauseMenu") as GameObject;
        pauseMenu = Instantiate(pauseMenu);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Destroy(pauseMenu);
        Destroy(this);
    }

    public void MainMenu()
    {
        Destroy(pauseMenu);
        Destroy(this);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Destroy(pauseMenu);
        Destroy(this);
        Application.Quit();
    }

    private void OnDestroy()
    {
        Destroy(pauseObject);
    }
}
