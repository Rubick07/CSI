using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image imageBar;
    private float fillAmount = 100f;
    // Start is called before the first frame update

    public void AddFill(float amount)
    {
        fillAmount += amount;
        fillAmount = Mathf.Clamp(fillAmount, 0, 100);

        imageBar.fillAmount = fillAmount/100f;
    }

    public void SubFill(float amount)
    {
        fillAmount -= amount;
        fillAmount = Mathf.Clamp(fillAmount, 0, 100);

        imageBar.fillAmount = fillAmount/100f;
    }

    public void ResetFill()
    {
        fillAmount = 0;
        imageBar.fillAmount = fillAmount;
    }

    public void SetFill(float amount)
    {
        fillAmount = amount;
        fillAmount = Mathf.Clamp(fillAmount, 0, 100);
        imageBar.fillAmount = fillAmount / 100f;
    }

}
