using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Computer : MonoBehaviour
{
    [SerializeField] GameObject PasswordUI;
    [SerializeField] GameObject UnlockedUI;
    [SerializeField] string Jawaban;
    [SerializeField] TMP_Text OutputText;
    private string input;

    public void ReadStringInput(string s)
    {
        input = s;
        CheckJawaban();
        Debug.Log(input);
    }

    public void CheckJawaban()
    {
        if (input.ToUpper() == Jawaban.ToUpper())
        {
            Debug.Log(input.ToUpper());
            Debug.Log("Kamu Benar");
            OutputText.text = "CORRECT";
            
        }
        else
        {
            OutputText.text = "INCORRECT";
            Debug.Log("Anjay salah");

        }
    }

    public void SetPassword(string password)
    {
        Jawaban = password;
    }


}
