using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text TimeText;

    private void Update()
    {
        //TimeText.text = GameManager.Instance.GetGamePlayingTimer().ToString("0");
        SetTime(GameManager.Instance.GetGamePlayingTimer());
    }

    private void SetTime(float oke)
    {
        int minutes = Mathf.FloorToInt(oke / 60);
        int second = Mathf.FloorToInt(oke % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }

}
