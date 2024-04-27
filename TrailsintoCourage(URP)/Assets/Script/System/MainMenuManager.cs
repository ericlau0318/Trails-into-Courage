using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private LoadingScene loadingScene;

    void Start()
    {
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
}
