using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1GameManager : MonoBehaviour
{
    private static int KilledEnemy;
    public Text KilledEnemyText;
    public GameObject WinPanel;
    void Start()
    {
        KilledEnemy = 0;
        WinPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        KilledEnemyText.text = "Killed Enemy: "+KilledEnemy +" / 10";
        if (KilledEnemy >= 10)
        {
            //Time.timeScale = 0;
            WinPanel.SetActive(true);
        }


    }
    public void AddKilledCount()
    {
        KilledEnemy++;
    }

    public void BackToMainTown()
    {
        SceneManager.LoadScene("Main Town");
    }
}
