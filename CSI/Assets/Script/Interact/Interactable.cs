using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Interactable : NetworkBehaviour
{
    public float interactRange;
    public GameObject Text;
    public LayerMask PlayerLayer;
    public Collider2D player;


    private void FixedUpdate()
    {
        if(PlayerInput.LocalInstance == null)
        {
            return;
        }

        player = Physics2D.OverlapCircle(transform.position, interactRange, PlayerLayer);


        if (player != null)
        {
            if(player.gameObject == PlayerInput.LocalInstance.gameObject)
            {
                Text.SetActive(true);

            }
            
        }
        else
        {
            Text.SetActive(false);
        }
    }

    public virtual void Interact()
    {
        Debug.Log("gk ada");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

}
