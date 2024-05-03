using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private LoadingScene loadingScene;
    public GameObject howToPlayPanel;
    [SerializeField]
    public static bool first;

    void Start()
    {
        howToPlayPanel.SetActive(false);
        loadingScene = FindObjectOfType<LoadingScene>();
        DataManager.Instance.LoadFirstVideo();
    }

    public void StartGame(int nextSceneIndex)
    {
        //SceneManager.LoadScene("Main Town");
        PlayVideo.first = true;
        loadingScene.LoadScene(nextSceneIndex);
    }
    public void ContinueGame(int nextSceneIndex)
    {
        if(!first)
        {
            loadingScene.LoadScene(nextSceneIndex);
            DataManager.Instance.LoadSavedData();
        }
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
