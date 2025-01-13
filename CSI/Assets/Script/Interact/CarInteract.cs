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

    new private void FixedUpdate()
    {
        if (Carsystem.carState != CarState.Ready)
            return;
        base.FixedUpdate();
    }

    public override void Interact()
    {
        if (!player.GetComponent<PlayerInput>().GetPickUpObject()) return;
        Debug.Log("CarInteract");
        //Carsystem.DeliverClue(player.GetComponent<PlayerInput>().GetPickUpObject());
        Text.SetActive(false);


        SendCarServerRpc();
        player.GetComponent<PlayerInput>().DeletePickUpObject();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
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
