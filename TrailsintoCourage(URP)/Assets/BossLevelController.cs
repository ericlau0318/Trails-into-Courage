using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossLevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        DataManager.Instance.AutoSave();
    }
    public void BacktoMainTown()
    {
        SceneManager.LoadScene("Main Town");
        DataManager.Instance.AutoSave();
    }
}
