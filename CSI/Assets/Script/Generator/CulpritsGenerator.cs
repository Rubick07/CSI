using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class CulpritsGenerator : NetworkBehaviour
{
    [SerializeField] CulpritSO[] _culprits;
    [SerializeField] List<int> _CulpritIndex;
    [SerializeField] TMP_Text _Culpritstxt;
    [SerializeField] float Countdown;

    private NetworkVariable<CulpritData> randomCulprit = new NetworkVariable<CulpritData>(new CulpritData
    {
        _int = null,
    }
, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    
    public struct CulpritData : INetworkSerializable
    {
        public int []_int;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
        }
    }


    private void Update()
    {
        if (!IsServer) return;

        //SelectCulpritClientRpc(randomCulprit);

    }
    public override void OnNetworkSpawn()
    {

        if (!IsServer)
        {
            randomCulprit.OnValueChanged += (CulpritData previousValue, CulpritData newValue) =>
            {
                randomCulprit.Value = new CulpritData { _int = _CulpritIndex.ToArray() };
            };

            _CulpritIndex.AddRange(randomCulprit.Value._int);
            foreach(int i in randomCulprit.Value._int)
            {
                Journal._instance.AddCulprits(_culprits[i]);
            }
            _Culpritstxt.text = _CulpritIndex[0].ToString();
            return;
        }
        Debug.Log("asdf");


        //_CulpritIndex = Random.Range(0, _culprits.Length);
        GenerateCulprit();
    }

    private void GenerateCulprit()
    {
        for (int i = 0; i < 5; i++)
        {
            int generateNumber = Random.Range(0, _culprits.Length);
            while (_CulpritIndex.Contains(generateNumber))
            {
                generateNumber = Random.Range(0, _culprits.Length);
            }
            _CulpritIndex.Add(generateNumber);

            Journal._instance.AddCulprits(_culprits[generateNumber]);
        }
        randomCulprit.Value = new CulpritData { _int = _CulpritIndex.ToArray() };
        _Culpritstxt.text = _CulpritIndex[0].ToString();
    }

    /*
    [ClientRpc (RequireOwnership = false)]
    private void SelectCulpritClientRpc(CulpritData index)
    {
        randomCulprit = index;
        _CulpritIndex.AddRange(randomCulprit._int);
        _Culpritstxt.text = _CulpritIndex[0].ToString();
    }
    */
    public CulpritSO GetCulprit()
    {
        return _culprits[_CulpritIndex[0]];
    }

    public List<int> getAllCulpritsIndex()
    {
        return _CulpritIndex;
    }

}
