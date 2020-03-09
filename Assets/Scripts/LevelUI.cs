using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public GameObject clock;
    private ClockUI clockUI;
    public TextMeshProUGUI pauseScreen;
    public TextMeshProUGUI gameOver;
    public Button restartButton;
    public Button startButton;
    public Button mapButton;
    private GlobalEventManager gem;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        clockUI = clock.GetComponent<ClockUI>();
        Time.timeScale = 1;
        gem.StartListening("LostGame", loseGame) ;
    }

    private void OnDestroy()
    {
        gem.StopListening("LostGame", loseGame);
    }

    public void startGame()
    {
        clock.SetActive(true);
        clockUI.runGame();
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

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void loseGame(GameObject target, List<object> parameters)
    {
        Time.timeScale = 0;
        gameOver.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        mapButton.gameObject.SetActive(false);
    }
}
