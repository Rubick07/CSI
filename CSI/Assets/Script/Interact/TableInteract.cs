using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum ClueProcessState
{
    idle,
    process,
    done
}

public class TableInteract : Interactable
{
    [SerializeField] private float _TimeToScan;
    float _timeTemp;
    [SerializeField] Animator animatorPopUpProcess;
    [SerializeField] Animator animatorPopUpDone;
    public ClueProcessState state;
    [SerializeField] ClueDonePickUp _BuktiObject;
    private GameObject _Bukti;
    private ClueDonePickUp ClueProcessReadyPickUp;

    private void Start()
    {
        _timeTemp = _TimeToScan;
        animatorPopUpProcess = GameObject.FindGameObjectWithTag("Notice").GetComponent<Animator>();
        animatorPopUpDone = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (!IsServer) return;
        if (!_Bukti) state = ClueProcessState.idle;
        if (state != ClueProcessState.process) return;

        if(_TimeToScan > 0) _TimeToScan -= Time.deltaTime;
        else if(state == ClueProcessState.process)
        {
            ClueProcessDone();
        }



    }

    private void ClueProcessDone()
    {
        animatorPopUpDone.SetTrigger("Process");
        ClueProcessReadyPickUp = Instantiate(_BuktiObject, transform);
        ClueProcessReadyPickUp.SetUpClue(_Bukti.GetComponent<PickUpInteract>().GetClue());
        ClueProcessReadyPickUp.GetComponent<NetworkObject>().Spawn(true);
        state = ClueProcessState.done;
        
        _Bukti = null;
    }
    

    public override void Interact()
    {
        TablePickUpServerRpc();     
    }

    
    [ClientRpc]
    public void TablePickUpClientRpc()
    {
        _Bukti = player.GetComponent<PlayerInput>().GetPickUpObject();
        if (_Bukti == null) return;
        _TimeToScan = _timeTemp;
        state = ClueProcessState.process;
        //Debug.Log(animatorPopUpProcess.GetCurrentAnimatorStateInfo(0).IsName("Flickering"));
        animatorPopUpProcess.SetTrigger("Process");
        player.GetComponent<PlayerInput>().DeletePickUpObject();
    } 

    [ServerRpc(RequireOwnership = false)]
    public void TablePickUpServerRpc()
    {
        TablePickUpClientRpc();
    }
    


}
