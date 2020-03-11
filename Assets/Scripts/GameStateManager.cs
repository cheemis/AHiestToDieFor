﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public TextMeshProUGUI pauseScreen;
    public TextMeshProUGUI gameOver;
    public Button restartButton;
    private GlobalEventManager gem;

    private float moneyCache;

    private void Awake()
    {
        List<MonoBehaviour> deps = new List<MonoBehaviour>
        {
            (gem = FindObjectOfType(typeof(GlobalEventManager)) as GlobalEventManager),
        };
        if (deps.Contains(null))
        {
            throw new Exception("Could not find dependency");
        }
        moneyCache = StaticMoney.GetMoneyCount();
        gem.StartListening("LostGame", LoseGame);
    }

    private void OnDestroy()
    {
        gem.StopListening("LostGame", LoseGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseScreen.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseScreen.gameObject.SetActive(false);
                restartButton.gameObject.SetActive(false);
            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void LoseGame(GameObject target, List<object> parameters)
    {
        StaticMoney.SetMoney(moneyCache);
        Time.timeScale = 0;
        gameOver.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }
}
