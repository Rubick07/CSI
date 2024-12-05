using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectCulpritUI : MonoBehaviour
{
    [SerializeField] Button[] culpritsSelectButton;
    [SerializeField] TMP_Text[] culpritsNameText;
    [SerializeField] Button confirmButton;
    [SerializeField] private List<CulpritSO> culpritslist;
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
            Debug.Log("Benar");
        }
        else
        {
            Debug.Log("Salah");
        }
    }

    public void ChangeSelectedculprit(int culpritIndex)
    {
        Debug.Log("keganti menjadi:" + culpritIndex);
        selectedculprits = culpritIndex;
    }

    private void OnGameOver(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver() && PlayerInput.LocalInstance.GetPlayerRole() == PlayerRole.Detektif)
        {
            Debug.Log("adf");
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

}
