using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PickUpInteract : Interactable
{

    public override void Interact()
    {
        PickUpServerRpc();
        //PlayerInput.LocalInstance.SetPickUpObject(gameObject);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    
    [ClientRpc]
    public void PickUpClientRpc()
    {
        player.GetComponent<PlayerInput>().SetPickUpObject(gameObject);
        //PlayerInput.LocalInstance.SetPickUpObject(gameObject);
        gameObject.SetActive(false);
    }

    [ServerRpc]
    public void PickUpServerRpc()
    {
        PickUpClientRpc();
    } 
    
}
