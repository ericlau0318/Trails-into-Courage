using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1GameManager : MonoBehaviour
{   
    [SerializeField]
    private static int KilledEnemy;
    public Text KilledEnemyText;
    public GameObject WinPanel;
    public int targetNumber=50;
    public bool fullfillTarget;
    public static bool IsLevel1Pass=false;
    void Start()
    {
        KilledEnemy = 0;
        fullfillTarget = false;
        WinPanel.SetActive(false);
        KilledEnemyText.text = "Killed Enemy: " +  "0 / " + targetNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (KilledEnemy >= targetNumber)
        {
            fullfillTarget = true;
            WinPanel.SetActive(true);
            IsLevel1Pass = true;
        }
    }
    public void AddKilledCount()
    {
        KilledEnemy++;        
        KilledEnemyText.text = "Killed Enemy: "+KilledEnemy +" / "+ targetNumber;
        Debug.Log(KilledEnemy);
        
    }

    public void BackToMainTown()
    {
        SceneManager.LoadScene("Main Town");
    }
}
