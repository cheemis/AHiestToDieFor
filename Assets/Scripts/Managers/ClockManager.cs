using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockManager : MonoBehaviour
{
    //controls each hand individually
    public GameObject clockFace;
    public GameObject clockMinuteHandTransform;
    public GameObject clockHourHandTransform;

    //this variable will keep track of in game time
    //use to scale guard flashlights over time
    public float day;

    //This will determine how long it takes to do a full clock rotation (12 to 12) in seconds
    public const float REAL_SECONDS_PER_INGAME_DAY = 360f;

    public float inGameTime;
    public bool gameIsRunning = false;
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
    private void Start()
    {
        gem.StartListening("StartGame", StartGame);
    }
    private void OnDestroy()
    {
        gem.StopListening("StartGame", StartGame);
    }
    private void StartGame(GameObject target, List<object> parameters)
    {
        gameIsRunning = true;
        clockFace.SetActive(true);
        clockHourHandTransform.SetActive(true);
        clockMinuteHandTransform.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsRunning)
        {
            day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

            float dayNormalized = day % 1f;

            float rotationDegreeesPerDay = 360f;
            clockHourHandTransform.transform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreeesPerDay * 2f);

            float hoursPerDay = 12f;
            clockMinuteHandTransform.transform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreeesPerDay * hoursPerDay * 2f);
        }

    }

    public void runGame()
    {
        gameIsRunning = true;
    }
}
