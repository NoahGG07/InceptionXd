using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Credits;
    public GameObject difficultyMenu;
    public GameObject menu;

    private GameManager gm;
    
    public void StartGame(int dificulty)
    {
        gm.difficulty = dificulty;
        SceneManager.LoadScene("Level1");
    }

    public void OpenSettings()
    {
        Settings.SetActive(true);
    }

    public void CloseSettings()
    {
        Settings.SetActive(false);
    }

    public void OpenCredits()
    {
        Credits.SetActive(true);
    }

    public void CloseCredits()
    {
        Credits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenDifficulty()
    {
        difficultyMenu.SetActive(true);
        menu.SetActive(false);
    }
    public void CloseDifficulty()
    {
        difficultyMenu.SetActive(false);
        menu.SetActive(true);
    }

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        menu.SetActive(true);
        difficultyMenu.SetActive(false);
        Credits.SetActive(false);
        Settings.SetActive(false);
    }
}
