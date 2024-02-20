using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1GameManager : MonoBehaviour
{   [SerializeField]
    private static int KilledEnemy;
    public Text KilledEnemyText;
    public GameObject WinPanel;
    public int targetNumber=50;
    void Start()
    {
        KilledEnemy = 0;
        WinPanel.SetActive(false);
        KilledEnemyText.text = "Killed Enemy: " +  "0 / " + targetNumber;
    }

    // Update is called once per frame
    void Update()
    {



    }
    public void AddKilledCount()
    {
        KilledEnemy++;        
        KilledEnemyText.text = "Killed Enemy: "+KilledEnemy +" / "+ targetNumber;
        if (KilledEnemy >= targetNumber)
        {
            //Time.timeScale = 0;
            WinPanel.SetActive(true);
        }
    }

    public void BackToMainTown()
    {
        SceneManager.LoadScene("Main Town");
    }
}
