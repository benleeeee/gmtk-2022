using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform creditsRef;
    public Transform mainMenuRef;
    private void Start()
    {
        mainMenuRef = GameObject.Find("MainMenu").transform;
        creditsRef = GameObject.Find("Credits").transform;
        creditsRef.gameObject.SetActive(false);
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsRef.gameObject.SetActive(true);
        mainMenuRef.gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        creditsRef.gameObject.SetActive(false);
        mainMenuRef.gameObject.SetActive(true);
    }

}
