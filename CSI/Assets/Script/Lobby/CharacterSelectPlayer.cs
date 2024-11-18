using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    private void Start()
    {
        CSIGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += CSIGameMultiplayer_OnPlayerDataNetworkListChanged;
    }

    private void CSIGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }



}
