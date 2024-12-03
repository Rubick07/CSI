using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectRole : MonoBehaviour
{
    [SerializeField] private PlayerRole playerRole;
    [SerializeField] private Button selectRoleButton;
    //[SerializeField] private Image image;
    //[SerializeField] private GameObject selectedGameObject;


    private void Awake()
    {
        selectRoleButton.onClick.AddListener(() => {
            CSIGameMultiplayer.Instance.ChangePlayerRole(playerRole);
        });
    }

    private void Start()
    {
        //CSIGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += KitchenGameMultiplayer_OnPlayerDataNetworkListChanged;
        //image.color = KitchenGameMultiplayer.Instance.GetPlayerColor(colorId);
        //UpdateIsSelected();
    }

    private void KitchenGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        //UpdateIsSelected();
    }
}
