using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : Data
{
    private StateController stateController;
    private Interactable interactable;
    public int knightFirstTalk = 1;

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
        interactable            =       FindObjectOfType<Interactable>();
    }
    public void AutoSave()
    {
        playerLevelValue        =       StateController.Level;
        expValue                =       StateController.Exp;
        statePointValue         =       StateController.StatePoint;
        strValue                =       StateController.STRValue;
        intValue                =       StateController.INTValue;
        hpValue                 =       StateController.HPValue;
        mpValue                 =       StateController.MPValue;
        spValue                 =       StateController.SPValue;

        SaveMagic();
        SaveLevelPass();

        PlayerPrefs.SetInt("PlayerLevel", playerLevelValue);
        PlayerPrefs.SetInt("Exp", expValue);
        PlayerPrefs.SetInt("StatePoint", statePointValue);
        PlayerPrefs.SetInt("STR", strValue);
        PlayerPrefs.SetInt("INT", intValue);
        PlayerPrefs.SetInt("HP", hpValue);
        PlayerPrefs.SetInt("MP", mpValue);
        PlayerPrefs.SetInt("SP", spValue);
        PlayerPrefs.Save();
    }

    public void LoadSavedData()
    {
        StateController.Level = PlayerPrefs.GetInt("PlayerLevel", playerLevelValue);
        StateController.Exp = PlayerPrefs.GetInt("Exp", expValue);
        StateController.StatePoint = PlayerPrefs.GetInt("StatePoint", statePointValue);
        StateController.STRValue = PlayerPrefs.GetInt("STR", strValue);
        StateController.INTValue = PlayerPrefs.GetInt("INT", intValue);
        StateController.HPValue = PlayerPrefs.GetInt("HP", hpValue);
        StateController.MPValue = PlayerPrefs.GetInt("MP", mpValue);
        StateController.SPValue = PlayerPrefs.GetInt("SP", spValue);

        LoadMagic();
        LoadLevelPass();
        stateController.UpdateUIForLoad();
    }

    public void SaveNPCTalk()
    {
        if(!interactable.isFirstTalk)
        {
            knightFirstTalk = 0;
        }
        else
            knightFirstTalk = 1;
        PlayerPrefs.SetInt("Knight_First_Talk", knightFirstTalk);
        PlayerPrefs.Save();
    }
    public void LoadNPCTalk() 
    {
        PlayerPrefs.GetInt("Knight_First_Talk", knightFirstTalk);
        if (knightFirstTalk == 0)
        {
            interactable.isFirstTalk = false;
        }
        else
            interactable.isFirstTalk = true;
    }
}
