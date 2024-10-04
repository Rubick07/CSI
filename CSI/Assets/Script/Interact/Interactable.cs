using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRange;
    public GameObject Text;
    public LayerMask PlayerLayer;
    public Collider2D player;
    private void FixedUpdate()
    {
        player = Physics2D.OverlapCircle(transform.position, interactRange, PlayerLayer);
        if (player != null)
        {
            Text.SetActive(true);
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
