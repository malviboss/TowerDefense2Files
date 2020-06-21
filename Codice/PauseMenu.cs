using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject GameOverUI, tower;


    public static bool GameIsOver = false, Restart;
     public AudioSource pauseAudio;
    
    void Start(){
        Restart = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else if (!GameIsOver)
            {
                Pause();
            }
        }

        if (TowerScript.lives <= 0)
        {
           // GameOver();
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        pauseAudio.Play();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        tower = GameObject.Find("Tower");
        GameOverUI.SetActive(false);
        TowerScript.isDead = false;
        SceneManager.LoadScene("Menu");
        Restart = true;
        
    }

    public void QuitGame()
    {
        Debug.Log("Uscita in corso...");
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

       void GameOver()
    {

      //  GameIsOver = true;
        
    }


}
