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
    public CarState carState;
    [SerializeField] float TimetoDeliver;
    private GameObject ClueToDeliver;
    private float TimetoDeliverTemp;
    private float TimetoReturn;

    private void Start()
    {
        TimetoDeliverTemp = TimetoDeliver;
        TimetoReturn = TimetoDeliverTemp;
    }

    private void Update()
    {
        if (!IsServer) return;

        if(carState == CarState.Deliver)
        {
            if(TimetoDeliver > 0)
            {
                TimetoDeliver -= Time.deltaTime;

            }
            else
            {
                ClueArrive();
            }

        }

        if(carState == CarState.Return)
        {
            if(TimetoReturn > 0)
            {
                TimetoReturn -= Time.deltaTime;
            }
            else
            {
                CarReturn();
            }
        }

    }

    public void DeliverClue(GameObject Clue)
    {
        if (carState != CarState.Ready) return;
        ClueToDeliver = Clue;
        carState = CarState.Deliver;
        TimetoDeliver = TimetoDeliverTemp;
    }

    public void ClueArrive()
    {
        ClueToDeliver.transform.position = TempatClueNyampe.position;
        //GameObject oke = Instantiate(ClueToDeliver, TempatClueNyampe);
        //oke.GetComponent<NetworkObject>().Spawn(true);
        //oke.transform.SetParent(TempatClueNyampe);
        //oke.transform.localPosition = new Vector3(0, 0, 0);        
        ClueToDeliver.SetActive(true);

        //ClueToDeliver.GetComponent<NetworkObject>().Despawn(true);
        //Destroy(ClueToDeliver);


        ClueToDeliver = null;
        carState = CarState.Return;
        TimetoReturn = TimetoDeliverTemp;
    }

    public void CarReturn()
    {
        carState = CarState.Ready;
    }


}
