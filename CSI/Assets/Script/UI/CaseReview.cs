using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class CaseReview : MonoBehaviour
{
    [SerializeField] private TMP_Text CulpritFoundText;
    [SerializeField] private TMP_Text EvidenceFoundText;
    [SerializeField] private TMP_Text TimeBonusText;
    [SerializeField] private TMP_Text CaseGradeText;
    [SerializeField] private TMP_Text CaseGradeTitleText;
    [SerializeField] private Button playAgainButton;
    private bool Benar;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(() => {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Jawaban(bool oke)
    {
        gameObject.SetActive(true);
        Benar = oke;
        SetCaseReview();
    }

    public void SetCaseReview()
    {
        int CaseReviewTotal = 0;
        if (Benar == false)
        {
            CulpritFoundText.text = "Wrong";
            

        }
        else
        {
            CulpritFoundText.text = "Correct";
            CaseReviewTotal += 200;
        }
        EvidenceFoundText.text = (Journal._instance.GetUnlockedEvidence() * 60).ToString();
        CaseReviewTotal += Journal._instance.GetUnlockedEvidence() * 60;

        if(CaseReviewTotal > 900)
        {
            CaseGradeText.text = "S";
            CaseGradeTitleText.text = "Ace Duo";
        }
        else if(CaseReviewTotal > 600 && CaseReviewTotal <= 900)
        {
            CaseGradeText.text = "A";
            CaseGradeTitleText.text = "Partners in Busting Crime";
        }
        else if(CaseReviewTotal > 300 && CaseReviewTotal < 600)
        {
            CaseGradeText.text = "B";
            CaseGradeTitleText.text = "Rookie Buddies";
        }
        else if(CaseReviewTotal <= 300)
        {
            CaseGradeText.text = "C";
            CaseGradeTitleText.text = "Trainees";
        }
    }

}
