using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    private void Awake()
    {
        serverBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
            StartGame();
        });

        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            StartGame();
        });

        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            StartGame();
        });

    }

    public void StartGame()
    {
        serverBtn.gameObject.SetActive(false);
        hostBtn.gameObject.SetActive(false);
        clientBtn.gameObject.SetActive(false);
    }
}
