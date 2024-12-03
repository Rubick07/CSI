using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PickUpInteract : Interactable
{
    [SerializeField] private Clue clue;
    public override void Interact()
    {
        if(player.GetComponent<PlayerInput>().GetPickUpObject() == null)
        {
            PickUpServerRpc();
        }

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

    [ServerRpc(RequireOwnership = false)]
    public void PickUpServerRpc()
    {
        PickUpClientRpc();
    }

    public Clue GetClue()
    {
        return clue;
    }
}

[System.Serializable]
public class Clue
{
    public string ClueName;
    public int imgIndex;
    [TextArea]
    public string description;


}
