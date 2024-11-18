using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MainMenuCleanUp : MonoBehaviour
{
    private void Awake()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
        if (CSIGameMultiplayer.Instance != null)
        {
            Destroy(CSIGameMultiplayer.Instance.gameObject);
        }
        if(CSIGameLobby.Instance != null)
        {
            Destroy(CSIGameLobby.Instance.gameObject);
        }
    }


}
