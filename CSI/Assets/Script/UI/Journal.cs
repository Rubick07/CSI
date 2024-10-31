using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum JournalState
{
    Profile,
    Evidence
}

public class Journal : MonoBehaviour
{
    [SerializeField] private JournalState state;
    [SerializeField] private GameObject Profile;
    [SerializeField] private GameObject Evidence;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Animator JournalAnimation;

    [SerializeField] private List<CulpritSO> culprits;
    [SerializeField] private List<Clue> cluesUnlocked;
    int index = 0;
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
        if (cluesUnlocked.Count == 0) return;

        image.sprite = cluesUnlocked[index].img;
        text.text = cluesUnlocked[index].description;

    }

    public void NextPage()
    {
        if (cluesUnlocked.Count == 0) return;

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
        if (cluesUnlocked.Count == 0) return;

        if (index == 0)
        {
            index = cluesUnlocked.Count;
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


    }

    public void ChangeToEvidence()
    {
        if (state == JournalState.Evidence) return;


    }


    public void PlayAnimation(string TriggerName)
    {
        JournalAnimation.SetTrigger(TriggerName);
    }

    public void AddCulprits(CulpritSO culpritSO)
    {
        culprits.Add(culpritSO);
    }

}


