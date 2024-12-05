using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.LobbyScene);
        });
        exitButton.onClick.AddListener(() => 
        {
            Application.Quit();
        });
    }

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
