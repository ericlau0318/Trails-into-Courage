using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2GameManager : MonoBehaviour
{
    public GameObject WinPanel;
    public static bool IsLevel2Pass=false;
    public static bool fullFillTarget = false;
    private void Start()
    {
        fullFillTarget = false;
    }
    public void BackToMainTown()
    {
        SceneManager.LoadScene("Main Town");
        DataManager.Instance.AutoSave();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            WinPanel.SetActive(true);
            IsLevel2Pass = true;
            fullFillTarget = true;

        }
    }
}
