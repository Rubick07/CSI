using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public enum JournalState
{
    Profile,
    Evidence,
    Case
}

[System.Serializable]
public class Page
{
    public string ClueName;
    public Sprite img;
    [TextArea]
    public string description;
}

public class Journal : NetworkBehaviour
{   
    [SerializeField] private JournalState state;

    [Header("Profile SetUp")]    
    [SerializeField] private GameObject ProfileUI;
    [SerializeField] private Image Profileimage;
    [SerializeField] private TMP_Text ProfileNametext;
    [SerializeField] private TMP_Text Descriptiontext;
    [SerializeField] private TMP_Text Alibitext;
    [Header("Evidence SetUp")]
    [SerializeField] private GameObject EvidenceUI;
    [SerializeField] private Button[] EvidenceSelectButton;
    [SerializeField] private TMP_Text Evidencetext;
    private int IndexAddedEvidence = 0;
    [Header("Case SetUp")]
    [SerializeField] private GameObject CaseUI;
    [SerializeField] private TMP_Text Case;
    [SerializeField] private TMP_Text CaseStory;
    [SerializeField] private TMP_Text AutopsyReportAtas;
    [SerializeField] private TMP_Text AutopsyReportBawah;
    [Header("Others")]
    [SerializeField] private Button NextButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Animator JournalAnimation;
    [SerializeField] private List<CulpritSO> culprits;
    [SerializeField] private List<Page> cluesUnlocked;
    int index = 0;

    [Header("CluePopUps SetUp")]
    [SerializeField] private TMP_Text ClueText;
    [SerializeField] private Image CluePhoto;

    //private NetworkVariable<JournalData>
    /*
    public struct JournalData : INetworkSerializable
    {
        public List<CulpritSO> culprits;
        public List<Clue> cluesUnlocked;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref culprits);
            serializer.SerializeValue(ref cluesUnlocked);
        }

    }
    */
    public static Journal _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddUnlockedClues(Page clue)
    {
        cluesUnlocked.Add(clue);
        int oke = IndexAddedEvidence;
        EvidenceSelectButton[oke].onClick.AddListener(() =>
        {
            Evidencetext.text = cluesUnlocked[oke].description;
        });
        EvidenceSelectButton[oke].interactable = true;
        EvidenceSelectButton[oke].GetComponent<Image>().sprite = cluesUnlocked[IndexAddedEvidence].img;
        
        IndexAddedEvidence++;
    }

    public void UpdatePage()
    {
        if(state == JournalState.Profile)
        {
            Profileimage.sprite = culprits[index].CulpritPhoto;
            ProfileNametext.text = culprits[index].CulpritName;
            Descriptiontext.text = culprits[index].CulpritDesc;
            //Alibitext.text = cluesUnlocked[index].description;
        }
        else if(state == JournalState.Evidence)
        {
            //Evidenceimage.sprite = cluesUnlocked[index].img;
            //Evidencetext.text = cluesUnlocked[index].description;

        }
        else
        {

        }

    }

    public void RefreshPage()
    {
        Profileimage.sprite = culprits[0].CulpritPhoto;
        ProfileNametext.text = culprits[0].CulpritName;

        //Evidenceimage.sprite = cluesUnlocked[0].img;
        Evidencetext.text = cluesUnlocked[0].description;
    }
    #region ButtonFunction
    public void NextPage()
    {

        if (index + 1 == culprits.Count)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        UpdatePage();

    }

    public void PrevPage()
    {

        if (index == 0)
        {
            index = culprits.Count -1;
        }
        else
        {
            index--;
        }
        UpdatePage();
    }


    public void ChangeToProfile()
    {
        if (state == JournalState.Profile) return;
        index = 0;
        state = JournalState.Profile;
        SetUIButton(true);
        ChangeUI(state);
        UpdatePage();
    }

    public void ChangeToEvidence()
    {
        if (cluesUnlocked.Count == 0) return;
        if (state == JournalState.Evidence) return;
        index = 0;
        state = JournalState.Evidence;
        SetUIButton(false);
        ChangeUI(state);
        UpdatePage();
    }

    public void ChangeToCase()
    {
        if (state == JournalState.Case) return;
        index = 0;
        state = JournalState.Case;
        SetUIButton(false);
        ChangeUI(state);
        UpdatePage();
    }

    #endregion
    public void PlayAnimation(string TriggerName)
    {
        JournalAnimation.SetTrigger(TriggerName);
    }

    public void AddCulprits(CulpritSO culpritSO)
    {
        culprits.Add(culpritSO);
    }

    public void CluePopUps(Page clue)
    {
        CluePhoto.sprite = clue.img;
        ClueText.text = clue.description;

    }

    public void ChangeUI(JournalState state)
    {
        if(state == JournalState.Profile)
        {
            ProfileUI.SetActive(true);
            EvidenceUI.SetActive(false);
            CaseUI.SetActive(false);
        }
        else if(state == JournalState.Evidence)
        {
            ProfileUI.SetActive(false);
            EvidenceUI.SetActive(true);
            CaseUI.SetActive(false);
        }
        else
        {
            ProfileUI.SetActive(false);
            EvidenceUI.SetActive(false);
            CaseUI.SetActive(true);
        }
    }

    public void SetUIButton(bool oke)
    {
        NextButton.gameObject.SetActive(oke);
        BackButton.gameObject.SetActive(oke);
    }

    public int GetUnlockedEvidence()
    {
        return cluesUnlocked.Count;
    }

}


