using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TestLobbyUI : NetworkBehaviour
{
    [SerializeField] private Button CreateButton;
    [SerializeField] private Button JoinButton;

    private void Awake()
    {
        CreateButton.onClick.AddListener(() =>{
            CSIGameMultiplayer.Instance.StartHost();
            Loader.LoadNetwork(Loader.Scene.CharacterSelectScene);
            }
        );
        JoinButton.onClick.AddListener(() => {
            CSIGameMultiplayer.Instance.StartClient();

        }
);
    }

}
