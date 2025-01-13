using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PickUpInteract : Interactable
{
    [SerializeField] private Clue clue;
    Page cluePage;
    public override void Interact()
    {
        
        if(player.GetComponent<PlayerInput>().GetPickUpObject() == null)
        {
            AudioManager.Instance.PlaySFX("ObjectPickUp");
            PickUpServer();
        }

        //PlayerInput.LocalInstance.SetPickUpObject(gameObject);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    
    /*
    [ClientRpc]
    public void PickUpClientRpc()
    {

        //player.GetComponent<PlayerInput>().SetPickUpObject(gameObject);
        //PlayerInventoryUI.Instance.SetImage(gameObject.GetComponent<SpriteRenderer>().sprite);
        //PlayerInput.LocalInstance.SetPickUpObject(gameObject);
        gameObject.SetActive(false);
    }

    [ServerRpc(RequireOwnership = false)]
    */
    public void PickUpServer()
    {
        //PickUpClientRpc();

        Journal._instance.AddUnlockedClues(clue);

    }

    public Clue GetClue()
    {
        return clue;
    }

    private void CluePageSetUp()
    {
        cluePage.ClueName = clued.Value.ClueName;
        cluePage.description = clue.Value.description;
        cluePage.img = ClueImages[clue.Value.IndexSprite];
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
