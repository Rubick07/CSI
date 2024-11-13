using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ClueDonePickUp : Interactable
{
    [SerializeField] Page cluePage;
    [SerializeField] Animator ClueReadyPickUp;
    [SerializeField] Sprite[] ClueImages;

    Clue clue;

    private NetworkVariable<ClueData> Cluedata = new NetworkVariable<ClueData>(new ClueData
    {
        ClueName = null,
        IndexSprite = 0,
        description = null,

    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct ClueData : INetworkSerializable
    {
        public string ClueName;
        public int IndexSprite;
        public string description;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ClueName);
            serializer.SerializeValue(ref IndexSprite);
            serializer.SerializeValue(ref description);
        }
    }


    private void Start()
    {
        ClueReadyPickUp = GameObject.FindGameObjectWithTag("PopUpDone").GetComponent<Animator>();
    }

    public override void Interact()
    {
        ClueReadyPickUp.SetTrigger("FadeIn");
        AddcluetoJournalServerRpc();
    }

    [ServerRpc (RequireOwnership = false)]
    public void AddcluetoJournalServerRpc()
    {
        AddcluetoJournalClientRpc();
    }

    [ClientRpc]
    public void AddcluetoJournalClientRpc()
    {
        Journal._instance.AddUnlockedClues(cluePage);
        Journal._instance.CluePopUps(cluePage);
        Journal._instance.RefreshPage();
        Destroy(gameObject);
    }

    public void SetUpClue(Clue newclue)
    {
        clue = newclue;


    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            Cluedata.OnValueChanged += (ClueData previousValue, ClueData newValue) =>
            {
                Cluedata.Value = newValue;
            };

            CluePageSetUp();

            return;
        }
        Cluedata.Value = new ClueData { ClueName = clue.ClueName, IndexSprite = clue.imgIndex, description = clue.description };

        CluePageSetUp();
    }

    private void CluePageSetUp()
    {
        cluePage.ClueName = Cluedata.Value.ClueName;
        cluePage.description = Cluedata.Value.description;
        cluePage.img = ClueImages[Cluedata.Value.IndexSprite];
    }
}
