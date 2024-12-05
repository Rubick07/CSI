using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileUI : MonoBehaviour
{
    [SerializeField] private Image PlayerProfile;
    [SerializeField] private Sprite[] PlayerSpriteProfile;

    private void Start()
    {
        if (PlayerInput.LocalInstance.GetPlayerRole() == PlayerRole.Detektif)
        {
            PlayerProfile.sprite = PlayerSpriteProfile[0];
        }
        else if(PlayerInput.LocalInstance.GetPlayerRole() == PlayerRole.Forensik)
        {
            PlayerProfile.sprite = PlayerSpriteProfile[1];
        }
    }

}
