using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Settings settings;
    public Sound[] musicSound;
    public Sound[] sfxSound;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject)
;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        settings.LoadSettings();
        musicSource.volume = settings.MusicSet;
        sfxSource.volume = settings.SFXSet;
    }
    public void PlayMusic(string name)
    {

        Sound s = Array.Find(musicSound, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }


    }

    public void PlaySFX(string name)
    {

        Sound s = Array.Find(sfxSound, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }

    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip clip;
}
