using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerPos : MonoBehaviour
{
    [SerializeField] Transform[] SpawnPos;
    
    public Transform GetPos(PlayerRole playerRole)
    {
        if(playerRole == PlayerRole.Detektif)
        {
            return SpawnPos[0];
        }
        else 
        {
            return SpawnPos[1];
        }
    }

}
