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
            NetworkManager.Singleton.StartHost();     
        
            }
        );
        JoinButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();

        }
);
    }

}
