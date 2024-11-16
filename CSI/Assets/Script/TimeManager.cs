using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TimeManager : NetworkBehaviour
{
    [SerializeField] private float TimeGameplay;
    bool GameIsstart;
    public static TimeManager instance;

    private Dictionary<ulong, bool> playerReadyDictionary;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerReadyDictionary = new Dictionary<ulong, bool>();

    }

    private void Update()
    {
        if (!IsServer) return;
        if (!GameIsstart) return;

        if (TimeGameplay > 0) TimeGameplay -= Time.deltaTime;
        else
        {
            TimeGameplay = 0;
        }

    }

    public void StartGame()
    {
        GameIsstart = true;
    }

}
