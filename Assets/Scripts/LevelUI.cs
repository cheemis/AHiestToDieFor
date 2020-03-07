using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public GameObject clock;
    private ClockUI clockUI;
    private GameObject startCamera;
    private GameObject mapCamera;
    // Start is called before the first frame update
    void Start()
    {
        clockUI = clock.GetComponent<ClockUI>();
        startCamera = GameObject.Find("MainCamera");
        mapCamera = GameObject.Find("MapCamera");
    }

    public void startGame()
    {
        clock.SetActive(true);
        clockUI.runGame();
    }

    public void mapButton()
    {
        mapCamera.SetActive(true);
        startCamera.SetActive(false);
    }

    public void backButton()
    {
        startCamera.SetActive(true);
        mapCamera.SetActive(false);
    }
}
