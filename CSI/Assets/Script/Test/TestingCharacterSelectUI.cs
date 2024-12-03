using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TestingCharacterSelectUI : MonoBehaviour
{
    [SerializeField] private Button ReadyButton;

    private void Awake()
    {
        ReadyButton.onClick.AddListener(() => {
            CharacterSelectReady.Instance.SetPlayerReady();
        }
        );
    }

}
