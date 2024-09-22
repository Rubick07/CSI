using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum PlayerState {idle, walk}
public enum PlayerRole {Detektif, Forensik }

public class PlayerInput : NetworkBehaviour
{
    public PlayerState state;
    public PlayerRole Role;
    [SerializeField] private float speed;
    Vector2 movement;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        
        
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

    }

    public override void OnNetworkSpawn()
    {
       
    }

}
