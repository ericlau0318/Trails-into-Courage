using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : Data
{
    private StateController stateController;
    private ShopPanelController shopPanelController;
    private static DataManager _instance;
    
    public static DataManager Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
            _instance = this;
        stateController         =       FindObjectOfType<StateController>();
    }
    public void AutoSave()
    {
        SavePlayerData();
        SaveMagic();
        SaveLevelPass();
        SaveFirstVideo();
        PlayerPrefs.Save();
    }
    public void SaveFirstVideo() 
    {
        if(PlayVideo.first)
        {
            firstStart = 1;
        }
        else
            firstStart = 0;
        PlayerPrefs.SetInt("FirstVideo", firstStart);
    }
    public void LoadFirstVideo()
    {
        PlayerPrefs.GetInt("FirstVideo", firstStart);
        if (firstStart == 1) 
        {
            PlayVideo.first = true;
            MainMenuManager.first = true;
        }
        else
        {
            PlayVideo.first = false;
            MainMenuManager.first = false;
        }

    }
    private void SavePlayerData()
    {
        playerLevelValue            =           StateController.Level;
        expValue                    =           StateController.Exp;
        statePointValue             =           StateController.StatePoint;
        strValue                    =           StateController.STRValue;
        intValue                    =           StateController.INTValue;
        hpValue                     =           StateController.HPValue;
        mpValue                     =           StateController.MPValue;
        spValue                     =           StateController.SPValue;

        PlayerPrefs.SetInt("PlayerLevel", playerLevelValue);
        PlayerPrefs.SetInt("Exp", expValue);
        PlayerPrefs.SetInt("StatePoint", statePointValue);
        PlayerPrefs.SetInt("STR", strValue);
        PlayerPrefs.SetInt("INT", intValue);
        PlayerPrefs.SetInt("HP", hpValue);
        PlayerPrefs.SetInt("MP", mpValue);
        PlayerPrefs.SetInt("SP", spValue);
    }

    public void LoadSavedData()
    {
        StateController.Level              =            PlayerPrefs.GetInt("PlayerLevel", playerLevelValue);
        StateController.Exp                =            PlayerPrefs.GetInt("Exp", expValue);
        StateController.StatePoint         =            PlayerPrefs.GetInt("StatePoint", statePointValue);
        StateController.STRValue           =            PlayerPrefs.GetInt("STR", strValue);
        StateController.INTValue           =            PlayerPrefs.GetInt("INT", intValue);
        StateController.HPValue            =            PlayerPrefs.GetInt("HP", hpValue);
        StateController.MPValue            =            PlayerPrefs.GetInt("MP", mpValue);
        StateController.SPValue            =            PlayerPrefs.GetInt("SP", spValue);

        LoadFirstVideo();
        LoadMagic();
        LoadLevelPass();
        stateController.UpdateUIForLoad();
        shopPanelController.UpdateMagicShopUI();
    }
}
