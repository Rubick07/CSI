using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ClueGenerator : NetworkBehaviour
{
    [SerializeField] private List<Transform> CluePosition;
    [SerializeField] private float StartTime;
    [SerializeField]private List<int> SelectedIndexCluePosition;
    private bool Isstart = false;


    private void Update()
    {
        if (!IsServer) return;
        
        if(StartTime > 0)
        {
            StartTime -= Time.deltaTime;
        }
        else if(!Isstart)
        {
            SpawnClue();
        }
        
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        for (int i = 0; i < 5; i++)
        {
            int Randomnumber = Random.Range(0, CluePosition.Count);
            while (SelectedIndexCluePosition.Contains(Randomnumber))
            {
                Randomnumber = Random.Range(0, CluePosition.Count);
            }

            SelectedIndexCluePosition.Add(Randomnumber);
        }
    }

    public void SpawnClue()
    {
       Isstart = true;
        SpawnClueClientRpc();
    }
    
    [ClientRpc]
    public void SpawnClueClientRpc()
    {
        CulpritSO SelectedCulprit = FindAnyObjectByType<CulpritsGenerator>().GetComponent<CulpritsGenerator>().GetCulprit();

        for (int i = 0; i < 5; i++)
        {
            var clue = Instantiate(SelectedCulprit.Bukti[i], CluePosition[SelectedIndexCluePosition[i]]);
            clue.GetComponent<NetworkObject>().Spawn(true);
        }
    }

}
