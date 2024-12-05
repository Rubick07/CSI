using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CarInteract : Interactable
{

    private Car Carsystem;

    private void Start()
    {
        Carsystem = FindAnyObjectByType<Car>().GetComponent<Car>();
    }
    public override void Interact()
    {
        if (!player.GetComponent<PlayerInput>().GetPickUpObject()) return;
        Debug.Log("CarInteract");
        //Carsystem.DeliverClue(player.GetComponent<PlayerInput>().GetPickUpObject());
        SendCarServerRpc();
        player.GetComponent<PlayerInput>().DeletePickUpObject();
    }

    [ServerRpc (RequireOwnership = false)]
    public void SendCarServerRpc()
    {
        SendCarClientRpc();
    }

    [ClientRpc]
    public void SendCarClientRpc()
    {
        Carsystem.DeliverClue(player.GetComponent<PlayerInput>().GetPickUpObject());
    }
}
