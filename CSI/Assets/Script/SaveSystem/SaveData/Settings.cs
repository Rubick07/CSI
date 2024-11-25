using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class Settings : ScriptableObject
{
    public float MusicSet;
    public float SFXSet;

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(this);
    }

    public void LoadSettings()
    {
        SettingData data = SaveSystem.LoadSettings();

        MusicSet = data.MusicSet;
        SFXSet = data.SFXSet;
    }

}
