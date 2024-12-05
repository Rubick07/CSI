using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class SelectCulpritUI : NetworkBehaviour
{
    [SerializeField] Button[] culpritsSelectButton;
    [SerializeField] TMP_Text[] culpritsNameText;
    [SerializeField] Button confirmButton;
    [SerializeField] private List<CulpritSO> culpritslist;
    [SerializeField] private CaseReview caseReview;
    private int selectedculprits;
    private Animator animator;
    public static SelectCulpritUI instance;
    CulpritsGenerator culpritsGenerator;
    int addedculprits;
    private void Start()
    {
        culpritsGenerator = FindAnyObjectByType<CulpritsGenerator>().GetComponent<CulpritsGenerator>();
        animator = GetComponent<Animator>();
        GameManager.Instance.OnStateChanged += OnGameOver;
    }
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

        confirmButton.onClick.AddListener(() =>
        {
            ConfirmTheCulprits();
        });
    }

    public void addCulprits(CulpritSO addSelectedCulprit)
    {
        Debug.Log(addSelectedCulprit);
        culpritslist.Add(addSelectedCulprit);
        culpritsNameText[addedculprits].text = addSelectedCulprit.CulpritName;
        culpritsSelectButton[addedculprits].GetComponent<Image>().sprite = addSelectedCulprit.CulpritPhoto;
        addedculprits++;
    }

    private void ConfirmTheCulprits()
    {
        if (culpritslist[selectedculprits] == culpritsGenerator.GetCulprit())
        {
            //Debug.Log("Benar");
            CasereviewServerRpc(true);
        }
        else
        {
            //Debug.Log("Salah");
            CasereviewServerRpc(false);
        }
        Hide();
    }

    public void ChangeSelectedculprit(int culpritIndex)
    {
        Debug.Log("keganti menjadi:" + culpritIndex);
        selectedculprits = culpritIndex;
    }

    private void OnGameOver(object sender, System.EventArgs e)
    {
        AudioManager.Instance.PlaySFX("CaseEnd");
        if (GameManager.Instance.IsGameOver() && PlayerInput.LocalInstance.GetPlayerRole() == PlayerRole.Detektif)
        {
            
            
            animator.SetTrigger("FadeIn");
        }
        else
        {
            Debug.Log("BukaGameOver");
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    [ServerRpc (RequireOwnership = false)]
    private void CasereviewServerRpc(bool oke)
    {
        CasereviewClientRpc(oke);
    }

    [ClientRpc]
    private void CasereviewClientRpc(bool oke)
    {
        Debug.Log(oke);
        Debug.Log(caseReview);
        caseReview.Jawaban(oke);
    }

}
