using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // player value set
    public int playerLevelValue;
    public int expValue;
    public int statePointValue;
    public int strValue, intValue, hpValue, mpValue, spValue;
    public Vector3 playerPositionValue;
    public bool story;
    // magic use type
    public int isFire, isIce, isFlash;
    // level pass set
    public int isLevel1, isLevel2, isLevel3;
    public int firstStart;

    public void SaveMagic()
    {
        if (ShopPanelController.spellFireUsing)
        {
            isFire = 1;
        }
        else
            isFire = 0;
        if (ShopPanelController.spellIceUsing)
        {
            isIce = 1;
        }
        else
            isIce = 0;
        if (ShopPanelController.spellFlashUsing)
        {
            isFlash = 1;
        }
        else
            isFlash = 0;
        PlayerPrefs.SetInt("FireUsing", isFire);
        PlayerPrefs.SetInt("IceUsing", isIce);
        PlayerPrefs.SetInt("FlashUsing", isFlash);
    }
    public void LoadMagic()
    {
        PlayerPrefs.GetInt("FireUsing", isFire);
        PlayerPrefs.GetInt("IceUsing", isIce);
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
    }
    public void SaveLevelPass()
    {
        // save level 1 pass
        if (Level1GameManager.IsLevel1Pass)
        { isLevel1 = 1; }
        else
            isLevel1 = 0;
        // save level 2 pass
        if (Level2GameManager.IsLevel2Pass)
        { isLevel2 = 1; }
        else
            isLevel2 = 0;
        //save level 3 pass 
        if (Level3GameManager.IsLevel3Pass)
        { isLevel3 = 1; }
        else
            isLevel3 = 0;

        PlayerPrefs.SetInt("Level_1", isLevel1);
        PlayerPrefs.SetInt("Level_2", isLevel2);
        PlayerPrefs.SetInt("Level_3", isLevel3);
    }
    public void LoadLevelPass()
    {
        PlayerPrefs.GetInt("Level_1", isLevel1);
        PlayerPrefs.GetInt("Level_2", isLevel2);
        PlayerPrefs.GetInt("Level_3", isLevel3);

        if (isLevel1 == 1)
        {
            Level1GameManager.IsLevel1Pass = true;
        }
        else
            Level1GameManager.IsLevel1Pass = false;

        if (isLevel2 == 1)
        {
            Level2GameManager.IsLevel2Pass = true;
        }
        else
            Level2GameManager.IsLevel2Pass = false;
        if (isLevel3 == 1)
        {
            Level3GameManager.IsLevel3Pass = true;
        }
        else
            Level3GameManager.IsLevel3Pass = false;
    }

}               