using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerPos : MonoBehaviour
{
    [SerializeField] Transform[] SpawnPos;
    
    public Transform GetPos(PlayerRole playerRole)
    {
        Debug.Log(playerRole);
        if(playerRole == PlayerRole.Detektif)
        {
            return SpawnPos[0];
        }
        else if(playerRole == PlayerRole.Forensik)
        {
            return SpawnPos[1];
        }

        return null;
    }

}
