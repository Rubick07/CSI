using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;
using System;

public enum PlayerState {idle, walk, OpenComputer}
public enum PlayerRole {Detektif, Forensik, Neutral}

public class PlayerInput : NetworkBehaviour
{
    [Header("PlayerState")]
    public PlayerState state;
    NetworkVariable <PlayerRole> Role = new NetworkVariable<PlayerRole>(PlayerRole.Neutral);
    [Header("PickUp SetUp")]
    [SerializeField] private GameObject PickUpObject;
    [SerializeField] private float speed;
    [SerializeField] private float InteractRange;
    [SerializeField] LayerMask InteractLayer;
    [Header("Camera SetUp")]
    [SerializeField] Camera Playercamera;
    Vector2 movement;
    Rigidbody2D rb;
    private bool OpenJournal = false;
    private Animator animator;

    public static PlayerInput LocalInstance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        Transform oke = FindAnyObjectByType<SpawnPlayerPos>().GetComponent<SpawnPlayerPos>().GetPos(Role.Value);
        transform.position = new Vector2(oke.position.x, oke.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            Playercamera.gameObject.SetActive(false);
            return;
        }
        if (GameManager.Instance.IsWaitingToStart())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SetLocalPlayerReady();
            }
            return;
        }
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }
        PlayerInputSystem();

    }

    private void PlayerInputSystem()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Journal._instance.UpdatePage();
            OpenCloseJournal();
        }
    }

    private void PauseGame()
    {
        GameManager.Instance.TogglePauseGame();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        if (state == PlayerState.OpenComputer) return;
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

    private void SetLocalPlayerReady()
    {
        GameManager.Instance.GameInputSetLocalPlayerReady();
    }

    private void OpenCloseJournal()
    {
        OpenJournal = !OpenJournal;

        if (OpenJournal) Journal._instance.PlayAnimation("Drop");
        else Journal._instance.PlayAnimation("Up");
    }

    public void ChangePlayerState(PlayerState NewplayerState)
    {
        state = NewplayerState;
    }

    public void ChangePlayerRole(PlayerRole NewplayerRole)
    {
        Role.Value = NewplayerRole;
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
            Debug.Log(LocalInstance);
        }
    }



    #region PickUpObject

    public GameObject GetPickUpObject()
    {       
        return PickUpObject;
    }

    public void SetPickUpObject(GameObject item)
    {
        PickUpObject = item;
    }

    public void DeletePickUpObject()
    {
        PickUpObject = null;
    }



    #endregion
}
