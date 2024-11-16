using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Computer : NetworkBehaviour
{
    [SerializeField] GameObject PasswordUI;
    [SerializeField] GameObject UnlockedUI;
    [SerializeField] string JawabanTerpilih;
    [SerializeField] string[] KumpulanJawaban;
    [SerializeField] TMP_Text OutputText;
    private string input;
    private int IndexTerpilih;
    private NetworkVariable<ComputerData> RandomPassword = new NetworkVariable<ComputerData>(new ComputerData
    {
        indexTerpilih = 0
    }
    , NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    public struct ComputerData: INetworkSerializable
    {
        public int indexTerpilih;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref indexTerpilih);
        }


    }


    public void ReadStringInput(string s)
    {
        input = s;
        CheckJawaban();
        Debug.Log(input);
    }

    public void CheckJawaban()
    {
        if (input.ToUpper() == JawabanTerpilih.ToUpper())
        {
            Debug.Log(input.ToUpper());
            PasswordUI.SetActive(false);
            UnlockedUI.SetActive(true);
            OutputText.text = "CORRECT";
            
        }
        else
        {
            OutputText.text = "INCORRECT";
        }
    }

    public void SetPassword(string password)
    {
        JawabanTerpilih = password;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            RandomPassword.OnValueChanged += (ComputerData previousValue, ComputerData newValue) =>
            {
                RandomPassword.Value = newValue;
            };
            IndexTerpilih = RandomPassword.Value.indexTerpilih;
            JawabanTerpilih = KumpulanJawaban[IndexTerpilih];
            return;
        }

        GeneratePassword();
    }

    private void GeneratePassword()
    {
        IndexTerpilih = Random.Range(0, KumpulanJawaban.Length);
        RandomPassword.Value = new ComputerData { indexTerpilih = IndexTerpilih };
        JawabanTerpilih = KumpulanJawaban[IndexTerpilih];
    }
}
