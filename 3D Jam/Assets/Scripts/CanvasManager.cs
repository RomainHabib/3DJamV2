﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject EscMenu, AlmostWinMenu, WinMenu, LooseMenu, AchievementsMenu, OptionsMenu,MainMenu;
    public bool stopTime;
    public static CanvasManager Instance { get; protected set; }
    private void Start()
    {
        stopTime = false;
        Time.timeScale = 1;

        if (Instance != null)
        {
            Debug.LogError("2 CanvasManager?");
        }
        Instance = this;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setEscMenu();
           
        }
        if (stopTime)
        {
            if(Time.timeScale > 0.1f)
            {
                Time.timeScale -= Time.deltaTime;
            }
        }
    }

    public void setWinMenu()
    {
        WinMenu.SetActive(!WinMenu.activeSelf);
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        stopTime = true;
    }

    public void setAlmostWinMenu()
    {
        AlmostWinMenu.SetActive(!AlmostWinMenu.activeSelf);
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        stopTime = true;

    }
    public void setEscMenu()
    {
        if (EscMenu.activeSelf == true)
        {
            Time.timeScale = 1;
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            Time.timeScale = 0;
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        EscMenu.SetActive(!EscMenu.activeSelf);

    }
    public void setLooseMenu()
    {
        LooseMenu.SetActive(!LooseMenu.activeSelf);
        Debug.Log("Perdu ! Looser");
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        stopTime = true;
    }

    public void retry()
    {
        SceneManager.LoadScene("Scene Final");
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void setAchievements()
    {
        AchievementsMenu.SetActive(!AchievementsMenu.activeSelf);
    }



    public void setOptionsMenu()
    {
        MainMenu.SetActive(!MainMenu.activeSelf);
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
