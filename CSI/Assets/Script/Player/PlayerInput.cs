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
    NetworkVariable <PlayerRole> Role = new NetworkVariable<PlayerRole>(PlayerRole.Detektif);
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetLocalPlayerReady();
            }
            return;
        }
        if (!GameManager.Instance.IsGamePlaying())
        {
            movement = Vector2.zero;
            return;
        }
        PlayerInputSystem();
        SetAnimation();
    }

    private void PlayerInputSystem()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.E))
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

    private void SetAnimation()
    {
        animator.SetFloat("Speed", MathF.Max(MathF.Abs(movement.x),MathF.Abs(movement.y)));
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("LastInputX", movement.x);
            animator.SetFloat("LastInputY", movement.y);
            
        }
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);
        AudioManager.Instance.PlaySFX("Walk");
    }

    private void SetLocalPlayerReady()
    {
        GameManager.Instance.GameInputSetLocalPlayerReady();
    }

    private void OpenCloseJournal()
    {
        OpenJournal = !OpenJournal;

        if (OpenJournal)
        {
            Journal._instance.PlayAnimation("Drop");
            
        }
        else
        {
            Journal._instance.PlayAnimation("Up");
            AudioManager.Instance.PlaySFX("JournalClose");
        }
    }

    public void ChangePlayerState(PlayerState NewplayerState)
    {
        state = NewplayerState;
    }

    public PlayerRole GetPlayerRole()
    {
        return Role.Value;
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

    public bool ThereisObject()
    {
        if(PickUpObject == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPickUpObject(GameObject item)
    {
        PickUpObject = item;
        PlayerInventoryUI.Instance.SetImage(item.GetComponent<SpriteRenderer>().sprite);
    }

    public void DeletePickUpObject()
    {
        PickUpObject = null;
        PlayerInventoryUI.Instance.DeleteImage();
    }



    #endregion
}
