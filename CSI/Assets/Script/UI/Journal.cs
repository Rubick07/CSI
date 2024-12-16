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
            if (CaseGenerator.instance.IsCase1())
            {
                Case.text = "Case: Planned Murder";
                CaseStory.text = "On a rainy day, 19th of September 2024, a man was found dead in the middle of the street. People directly went to the body and called for help, but even before calling the ambulance the man was already lifeless with little to no resistance. Not a single person was found nearby and there are 5 main suspects to this case.";
                AutopsyReportAtas.text = "The person is founded in a dead condition on 6:30 PM on Kerain Square. There are indications of some bruise marks on his forehead and some traces of a substance in his coat. There were no traces of fingerprints nor blood found on his body.";
            }
            else if (CaseGenerator.instance.IsCase2())
            {
                Case.text = "Case: Unplanned Murder";
                CaseStory.text = "10th of May 2022, a corpse was found inside of a somewhat luxurious hotel. The body was found by the bellboy attempting to clean the room. Above the table was found traces of medicines with 2 diary books and a suicide note. The police force deemed it as a suicide case, but is it really the case? There are 5 main suspects on this case.";
                AutopsyReportAtas.text = "The victim was found hanging on with a rope in his neck, but his body was found stabbed by a collection of decorative swords on the wall with marks of multiple small stabbings on some parts of the body, making the cause of death unclear.";
            }
            else if (CaseGenerator.instance.IsCase3())
            {
                Case.text = "Case: Theft";
                CaseStory.text = "On the 7th of January 2023, on Kerain bank, a theft case happened in a bank where the culprit is still unidentified. It was supposed to be a regular saturday with people coming all over the bank. Suddenly, a bank teller screamed out and found out that some of their prized posession as well as bags of cash has disappeared from the bank. Lucky for the bank, and unlucky for the victim, the dynamic duo is here to crack the case.";
                AutopsyReportAtas.text = "There are several missing items on the bank. The items include some electronics, bags of cash, and a red bag containing gold for the banks' grand prize in an event. Other than that, some jewelries are also missing from the employees' locker room. The crime was noticed around 12.00, approximately after the operational hour of the bank reaches break time.";
            }
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


