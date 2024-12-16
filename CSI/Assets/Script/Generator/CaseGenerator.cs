using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public enum Case
{
    Case1,Case2,Case3
}

public class CaseGenerator : NetworkBehaviour
{
    public Case JenisKasus;
    [SerializeField] TMP_Text Casetxt;
    public static CaseGenerator instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private NetworkVariable<CaseData> randomCase = new NetworkVariable<CaseData>(new CaseData
    {
        KasusTerpilih = Case.Case1,
    }
, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct CaseData : INetworkSerializable
    {
        public Case KasusTerpilih;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref KasusTerpilih);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            randomCase.OnValueChanged += (CaseData previousValue, CaseData newValue) =>
            {
                randomCase.Value = newValue;
            };
            JenisKasus = randomCase.Value.KasusTerpilih;
            Casetxt.text = randomCase.Value.KasusTerpilih.ToString();
            return;
        }

        GenerateCase();
    }

    private void GenerateCase()
    {
        int RandomNumber = Random.Range(0,3);
        JenisKasus = (Case)RandomNumber;
        randomCase.Value = new CaseData {KasusTerpilih = JenisKasus};
        Casetxt.text = JenisKasus.ToString();
        Debug.Log(randomCase.Value.KasusTerpilih);
    }

    public bool IsCase1()
    {
        return JenisKasus == Case.Case1;
    }

    public bool IsCase2()
    {
        return JenisKasus == Case.Case2;
    }

    public bool IsCase3()
    {
        return JenisKasus == Case.Case3;
    }

}
