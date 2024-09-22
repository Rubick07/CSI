using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("MainMenu");
    }

    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
        
    }

    public void QuitGame()
    {
        Debug.Log("Keluar");
        Application.Quit();
    }

}
