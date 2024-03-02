using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : Data
{
    private StateController stateController;
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
        stateController = FindObjectOfType<StateController>();
    }
    public void AutoSave()
    {
        playerLevelValue = StateController.Level;
        expValue = StateController.Exp;
        statePointValue = StateController.StatePoint;
        strValue = StateController.STRValue;
        intValue = StateController.INTValue;
        hpValue = StateController.HPValue;
        mpValue = StateController.MPValue;
        spValue = StateController.SPValue;
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
        stateController.UpdateUIForLoad();
    }

}
