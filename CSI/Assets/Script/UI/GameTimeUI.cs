using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text TimeText;

    private void Update()
    {
        TimeText.text = GameManager.Instance.GetGamePlayingTimer().ToString("0");
    }


}
