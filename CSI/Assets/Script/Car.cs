using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum CarState
{
    Ready,
    Deliver,
    Return
}

public class Car : NetworkBehaviour
{
    [SerializeField] Transform TempatClueNyampe;
    [SerializeField] private Bar processBar;
    public CarState carState;
    [SerializeField] float TimetoDeliver;
    private GameObject ClueToDeliver;
    private float TimetoDeliverTemp;
    private float TimetoReturn;
    CarInteract carInteract;
    private void Start()
    {
        TimetoDeliverTemp = TimetoDeliver;
        TimetoReturn = TimetoDeliverTemp;
        carInteract = FindAnyObjectByType<CarInteract>();
    }

    private void Update()
    {
        if (!IsServer) return;

        if(carState == CarState.Deliver)
        {
            if(TimetoDeliver > 0)
            {
                TimetoDeliver -= Time.deltaTime;
                float TimeLeft = ((TimetoDeliverTemp - TimetoDeliver) / (TimetoDeliverTemp )) * 100;
                processBar.SetFill(TimeLeft);
            }
            else
            {
                ClueArriveServerRpc();
            }

        }

        if(carState == CarState.Return)
        {
            if(TimetoReturn > 0)
            {
                TimetoReturn -= Time.deltaTime;
                float TimeLeft = ((TimetoDeliverTemp - TimetoReturn ) / (TimetoDeliverTemp )) * 100;
                processBar.SetFill(TimeLeft);
            }
            else
            {
                CarReturnServerRpc();
            }
        }

    }

    public void DeliverClue(GameObject Clue)
    {
        if (carState != CarState.Ready) return;

        processBar.gameObject.SetActive(true);
        ClueToDeliver = Clue;
        carState = CarState.Deliver;
        TimetoDeliver = TimetoDeliverTemp;
    }

    [ClientRpc]
    public void ClueArriveClientRpc()
    {
        ClueToDeliver.transform.position = TempatClueNyampe.position;  
        ClueToDeliver.SetActive(true);

        ClueToDeliver = null;
        carState = CarState.Return;
        TimetoReturn = TimetoDeliverTemp;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ClueArriveServerRpc()
    {
        ClueArriveClientRpc();
    }

    [ClientRpc]
    public void CarReturnClientRpc()
    {
        carState = CarState.Ready;
        carInteract.Text.SetActive(true);
        processBar.gameObject.SetActive(false);
        carInteract.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
    }

    [ServerRpc]
    public void CarReturnServerRpc()
    {
        CarReturnClientRpc();
    }


}
