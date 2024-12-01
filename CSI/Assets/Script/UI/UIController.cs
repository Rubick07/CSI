using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _SfxSlider;
    public Settings settings;
    private void Start()
    {
        //_musicSlider.value = AudioManager.Instance.musicSource.volume;
        //_SfxSlider.value = AudioManager.Instance.sfxSource.volume;
    }

    public void Pause(bool Pause)
    {
        if (Pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    #region SaveData

    public void SaveSettings()
    {
        settings.SaveSettings();
    }

    #endregion

    #region sound
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
        settings.MusicSet = _musicSlider.value;
    }

    public void SfxVolume()
    {
        AudioManager.Instance.SFXVolume(_SfxSlider.value);
        settings.SFXSet = _SfxSlider.value;
    }
    #endregion
}
