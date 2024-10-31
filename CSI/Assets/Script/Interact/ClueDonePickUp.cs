using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueDonePickUp : Interactable
{
    [SerializeField] Clue clue;
    [SerializeField] Animator ClueReadyPickUp;
    

    public override void Interact()
    {
        Journal._instance.AddUnlockedClues(clue);
        Destroy(gameObject);
    }

    public void SetUpClue(Clue newclue)
    {
        clue = newclue;
    }
}
