using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ClueDonePickUp : Interactable
{
    [SerializeField] Clue clue;
    [SerializeField] Animator ClueReadyPickUp;
    
    private void Start()
    {
        ClueReadyPickUp = GameObject.FindGameObjectWithTag("PopUpDone").GetComponent<Animator>();
    }

    public override void Interact()
    {
        AddcluetoJournalServerRpc();
    }

    [ServerRpc (RequireOwnership = false)]
    public void AddcluetoJournalServerRpc()
    {
        AddcluetoJournalClientRpc();
    }

    [ClientRpc]
    public void AddcluetoJournalClientRpc()
    {
        Journal._instance.AddUnlockedClues(clue);
        Journal._instance.CluePopUps(clue);
        Journal._instance.RefreshPage();
        ClueReadyPickUp.SetTrigger("FadeIn");
        Destroy(gameObject);
    }

    public void SetUpClue(Clue newclue)
    {
        clue = newclue;
    }
}
