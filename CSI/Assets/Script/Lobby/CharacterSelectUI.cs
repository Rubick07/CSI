using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Services.Lobbies.Models;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private Button readyButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text lobbyNametext;
    [SerializeField] private TMP_Text lobbyCodetext;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>{
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.LobbyScene);

        }        
        );

        readyButton.onClick.AddListener(() => {
            CharacterSelectReady.Instance.SetPlayerReady();
        }
        );

    }

    private void Start()
    {
        Lobby lobby = CSIGameLobby.Instance.GetLobby();

        lobbyNametext.text = "Lobby Name: " + lobby.Name;
        lobbyCodetext.text = "Lobby Code: " + lobby.LobbyCode;
    }
}
