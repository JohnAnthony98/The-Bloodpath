using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    //Menus & UI
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject youWin;
    public GameObject controlsMenu;
    public GameObject deathCounter;
    public GameObject dashCounter;
    public GameObject blastCounter;
    public GameObject healthCounter;
    public TextMeshProUGUI deathsText;

    private bool paused;

    void Awake()
    {
        //singleton class
        if (gameController != null)
        {
            if (gameController != this)
            {
                Destroy(this);
            }
        }
        else
        {
            gameController = this;
        }
        DontDestroyOnLoad(this);

        if (pauseMenu == null)
        {
            Debug.Log("Need to Instantiate Pause Menu in GameController");
            Application.Quit();
        }
        pauseMenu.SetActive(false);
        paused = false;
        youWin.SetActive(false);
        deathCounter.SetActive(false);
        dashCounter.SetActive(false);
        blastCounter.SetActive(false);
        healthCounter.SetActive(false);

        //set Controls Menu to inactive
        controlsMenu.SetActive(false);

        if (mainMenu == null)
        {
            Debug.Log("Need to Instantiate Main Menu in GameController");
            Application.Quit();
        }
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) // do this so long as you arent on the main menu
        {
            int numDashes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetDashes();
            int deaths = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetDeaths();
            int numBlasts = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetBlasts();
            int playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetHealth();

            UILoop(3, dashCounter, numDashes);
            UILoop(2, blastCounter, numBlasts);
            UILoop(3, healthCounter, playerHealth);

            deathsText.text = "" + deaths;

            if (Input.GetButtonDown("Pause"))
            {
                //Debug.Log("Escape Pressed");
                PauseGame();
            }
        }
    }

    void UILoop(int num, GameObject UI, int action)
    {
        for (int i = 0; i < num; ++i)
        {
            UI.transform.GetChild(i).gameObject.SetActive(false);
        }
        UI.transform.GetChild((num - 1) - action).gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
    }

    private void PauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }

    public void MainMenu()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        youWin.SetActive(false);
        youWin.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void SeeControls()
    {
        PauseGame();
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void LeaveControls()
    {
        controlsMenu.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        //Debug.Log("Start Button Pressed");
        if (SceneManager.GetActiveScene().buildIndex == 0) // Main Menu
        {
            mainMenu.SetActive(false);
            paused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
            deathCounter.SetActive(true);
            dashCounter.SetActive(true);
            blastCounter.SetActive(true);
            healthCounter.SetActive(true);
        }
        else
        {
            Debug.Log("Error::Started game while game was already started");
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        youWin.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        //Debug.Log("Quit Button Pressed");

        Application.Quit();
    }

    public void OpenFeedBack()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSf2-3nbO2rYuwUjKx9g2HC7zoROMgxLUy3_KyHg6Lw44TVh_w/viewform?usp=sf_link");
    }

    public bool GamePaused()
    {
        return paused;
    }

    public void PlayerWins()
    {
        youWin.SetActive(true);
        Time.timeScale = 0;
        //if there are no more levels
        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            //set NextLevel button to inactive
            youWin.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
