using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private LoadingScene loadingScene;
    public GameObject howToPlayPanel;

    void Start()
    {
        howToPlayPanel.SetActive(false);
        loadingScene = FindObjectOfType<LoadingScene>();
    }

    void Update()
    {
        
    }

    public void StartGame(int nextSceneIndex)
    {
        //SceneManager.LoadScene("Main Town");
        loadingScene.LoadScene(nextSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void openHowtoPlay()
    {
        if (!howToPlayPanel.activeSelf)
        {
            howToPlayPanel.SetActive(true);
        }
        else { 
            howToPlayPanel.SetActive(false);
        }
    }

    public void closeHowtoPlay()
    {
        howToPlayPanel.SetActive(false);
    }
}
