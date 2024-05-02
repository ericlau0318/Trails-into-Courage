using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public static void SaveState(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool LoadState(string key, bool defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }
}
