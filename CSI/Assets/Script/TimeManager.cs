using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TimeManager : NetworkBehaviour
{
    [SerializeField] private float TimeGameplay;
    bool GameIsstart;
    public static TimeManager instance;

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
