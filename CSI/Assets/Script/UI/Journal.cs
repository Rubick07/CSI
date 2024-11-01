using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public enum JournalState
{
    Profile,
    Evidence
}

public class Journal : MonoBehaviour
{   
    [SerializeField] private JournalState state;

    [Header("Profile SetUp")]    
    [SerializeField] private GameObject Profile;
    [SerializeField] private Image Profileimage;
    [SerializeField] private TMP_Text ProfileNametext;
    [SerializeField] private TMP_Text Alibitext;
    [Header("Evidence SetUp")]
    [SerializeField] private GameObject Evidence;
    [SerializeField] private Image Evidenceimage;
    [SerializeField] private TMP_Text Evidencetext;
    
    [SerializeField] private Animator JournalAnimation;
    [SerializeField] private List<CulpritSO> culprits;
    [SerializeField] private List<Clue> cluesUnlocked;
    int index = 0;

    [Header("CluePopUps SetUp")]
    [SerializeField] private TMP_Text ClueText;
    [SerializeField] private Image CluePhoto;

    //private NetworkVariable<JournalData>

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

    public void AddUnlockedClues(Clue clue)
    {
        cluesUnlocked.Add(clue);
    }

    public void UpdatePage()
    {
        if(state == JournalState.Profile)
        {
            Profileimage.sprite = culprits[index].CulpritPhoto;
            ProfileNametext.text = culprits[index].CulpritName;
            //Alibitext.text = cluesUnlocked[index].description;
        }
        else if(state == JournalState.Evidence)
        {
            Evidenceimage.sprite = cluesUnlocked[index].img;
            Evidencetext.text = cluesUnlocked[index].description;

        }

    }

    public void RefreshPage()
    {
        Profileimage.sprite = culprits[0].CulpritPhoto;
        ProfileNametext.text = culprits[0].CulpritName;

        Evidenceimage.sprite = cluesUnlocked[0].img;
        Evidencetext.text = cluesUnlocked[0].description;
    }

    public void NextPage()
    {

        if (index + 1 == cluesUnlocked.Count)
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
            index = cluesUnlocked.Count -1;
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
        Profile.SetActive(true);
        Evidence.SetActive(false);
        state = JournalState.Profile;
        UpdatePage();
    }

    public void ChangeToEvidence()
    {
        if (cluesUnlocked.Count == 0) return;
        if (state == JournalState.Evidence) return;
        index = 0;
        Profile.SetActive(false);
        Evidence.SetActive(true);
        state = JournalState.Evidence;
        UpdatePage();
    }


    public void PlayAnimation(string TriggerName)
    {
        JournalAnimation.SetTrigger(TriggerName);
    }

    public void AddCulprits(CulpritSO culpritSO)
    {
        culprits.Add(culpritSO);
    }

    public void CluePopUps(Clue clue)
    {
        CluePhoto.sprite = clue.img;
        ClueText.text = clue.description;

    }

}


