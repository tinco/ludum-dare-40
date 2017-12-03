using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;

    public void Start()
    {
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }

    public void StartNewGamePress()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }

    public void BackButtonPressed()
    {
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }

    public void QuitButtonPressed()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void LevelPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
