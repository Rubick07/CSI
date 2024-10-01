using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public enum PlayerState {idle, walk}
public enum PlayerRole {Detektif, Forensik }

public class PlayerInput : NetworkBehaviour
{
    public PlayerState state;
    public PlayerRole Role;
    [SerializeField] private float speed;
    [SerializeField] private float InteractRange;
    [SerializeField] LayerMask InteractLayer;
    Vector2 movement;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Transform oke = FindAnyObjectByType<SpawnPlayerPos>().GetComponent<SpawnPlayerPos>().GetPos((int)OwnerClientId);
        transform.position = new Vector2(oke.position.x, oke.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
        
        
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);



    }

    private void Interact()
    {
        Collider2D item = Physics2D.OverlapCircle(transform.position, InteractRange, InteractLayer);

        if (item != null)
        {
            Interactable barang = item.GetComponent<Interactable>();
            barang.Interact();
        }

    }

    public override void OnNetworkSpawn()
    {
        
        //Transform oke = FindAnyObjectByType<SpawnPlayerPos>().GetComponent<SpawnPlayerPos>().GetPos((int)OwnerClientId);
        //transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
        
    }

    

}
