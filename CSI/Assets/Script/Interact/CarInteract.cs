using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Carsystem.DeliverClue(player.GetComponent<PlayerInput>().GetPickUpObject());
        player.GetComponent<PlayerInput>().DeletePickUpObject();
    }

}
