using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3GameManager : MonoBehaviour
{
    public GameObject WinPanel;
    public static bool IsLevel3Pass = true;
    public void BackToMainTown()
    {
        SceneManager.LoadScene("Main Town");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WinPanel.SetActive(true);
            IsLevel3Pass = true;
        }
    }
}
