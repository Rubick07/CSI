using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    public float MusicSet;
    public float SFXSet;

    public SettingData(Settings settings)
    {
        MusicSet = settings.MusicSet;
        SFXSet = settings.SFXSet;
    }


}