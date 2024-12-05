using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThemeSong : MonoBehaviour
{
    [SerializeField] private string SongName;
    [SerializeField] private string SFXString;
    void Start()
    {
        AudioManager.Instance.PlayMusic(SongName); 
    }

    public void PlaySFXSound(string Oke)
    {
        AudioManager.Instance.PlaySFX(Oke);
    }
}
