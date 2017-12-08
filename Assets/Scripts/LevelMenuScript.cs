using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuScript : MonoBehaviour {

    public GameObject PauseMenu;
    //public GameObject DeathMenu;
    public GameObject LevelCompleteMenu;
    public Text TimeCardText;

    public virtual void Start()
    {
        PauseMenu.SetActive(false);
        //DeathMenu.SetActive(false);
        LevelCompleteMenu.SetActive(false);
    }

    public void RestartPressed()
    {

        FindObjectOfType<GameController>().BroadcastMessage("OnRestart");
        PauseMenu.SetActive(false);
        //DeathMenu.SetActive(false);
        LevelCompleteMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void BackButtonPressed()
    {
        PauseMenu.SetActive(false);
        //DeathMenu.SetActive(false);
        LevelCompleteMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void LevelPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
