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
        shopPanelController     =       FindObjectOfType<ShopPanelController>();
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
  
        if(ShopPanelController.spellFireUsing) 
        {
            isFire = 1;
        }
        else
            isFire = 0;
        if(ShopPanelController.spellIceUsing)
        {
            isIce = 1;
        }
        else 
            isIce = 0;
        if(ShopPanelController.spellFlashUsing) 
        {
            isFlash = 1;
        }
        else
            isFlash = 0;

        PlayerPrefs.SetInt("PlayerLevel", playerLevelValue);
        PlayerPrefs.SetInt("Exp", expValue);
        PlayerPrefs.SetInt("StatePoint", statePointValue);
        PlayerPrefs.SetInt("STR", strValue);
        PlayerPrefs.SetInt("INT", intValue);
        PlayerPrefs.SetInt("HP", hpValue);
        PlayerPrefs.SetInt("MP", mpValue);
        PlayerPrefs.SetInt("SP", spValue);
        PlayerPrefs.SetInt("FireUsing", isFire  );
        PlayerPrefs.SetInt("IceUsing", isIce    );
        PlayerPrefs.SetInt("FlashUsing", isFlash);
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
        PlayerPrefs.GetInt("FireUsing", isFire);
        PlayerPrefs.GetInt("IceUsing", isIce    );
        PlayerPrefs.GetInt("FlashUsing", isFlash);

        //set fire magic for use
        if (PlayerPrefs.GetInt("FireUsing", isFire) == 1)
            ShopPanelController.spellFireUsing = true;
        else
            ShopPanelController.spellFireUsing = false;
        // set ice magic for use
        if (PlayerPrefs.GetInt("IceUsing", isIce) == 1)
            ShopPanelController.spellIceUsing = true;
        else
            ShopPanelController.spellIceUsing = false;
        // set flash magic for use
        if (PlayerPrefs.GetInt("FlashUsing", isFlash) == 1)
            ShopPanelController.spellFlashUsing = true;
        else
            ShopPanelController.spellFlashUsing = false;


        stateController.UpdateUIForLoad();
    }
}
