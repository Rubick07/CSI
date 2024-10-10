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
        if (PlayerInput.LocalInstance.GetPickUpObject() == null) return;
        //Debug.Log(PlayerInput.LocalInstance.GetPickUpObject());
        Carsystem.DeliverClue(player.GetComponent<PlayerInput>().GetPickUpObject());
    }

}
