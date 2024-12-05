using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteract : Interactable
{
    [SerializeField] private CanvasGroup ComputerUI;

    private PlayerInput LastPlayerInput;
    public override void Interact()
    {
        AudioManager.Instance.PlaySFX("ComputerOn");
        LastPlayerInput = player.GetComponent<PlayerInput>();
        LastPlayerInput.ChangePlayerState(PlayerState.OpenComputer);
        ComputerUI.alpha = 1;
        ComputerUI.interactable = true;
        ComputerUI.blocksRaycasts = true;
    }

    public void LeaveComputer()
    {
        LastPlayerInput.ChangePlayerState(PlayerState.idle);
    }

}
