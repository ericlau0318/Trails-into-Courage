using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2GameManager : MonoBehaviour
{
    public GameObject WinPanel;
    public static bool IsLevel2Pass=false;
    public void BackToMainTown()
    {
        SceneManager.LoadScene("Main Town");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            WinPanel.SetActive(true);
            IsLevel2Pass = true;
        }
    }


}
