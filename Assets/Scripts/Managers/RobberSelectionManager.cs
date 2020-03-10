using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class RobberSelectionManager : MonoBehaviour
{
    public GameObject[] robberPrefabs;
    public Sprite[] robberList;
    public Button[] buttonList;


    private GameObject[] selectedRobbers;
    private Button button;
    private int slotNum = -1;
    private int robberNum = -1;

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
        selectedRobbers = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        //displays image over button
        // Only go into this if statement
        // if the player has enough money to buy anything to begin with
        if (robberNum > -1 && slotNum > -1)
        {
            button = buttonList[slotNum];
            button.image.sprite = robberList[robberNum];
            selectedRobbers[slotNum] = robberPrefabs[robberNum];
            button.transform.GetChild(1).gameObject.SetActive(false);
            button.transform.GetChild(0).gameObject.SetActive(false);

            //shows next button
            //TODO: dont show button if player can't afford
            if (slotNum < 3)
            {
                button = buttonList[slotNum + 1];
                button.transform.gameObject.SetActive(true);
            }

            //reset
            robberNum = -1;
            slotNum = -1;
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
        gameObject.SetActive(false);
    }
    public void AttemptStartGame()
    {
        if (selectedRobbers.All(robber => robber == null))
        {
            return;
        }
        List<GameObject> filteredSelection = selectedRobbers.Where(robber => robber != null).ToList();
        foreach (GameObject robberPrefab in filteredSelection)
        {
            if (robberPrefab != null)
            {
                Debug.Log(robberPrefab.name);
            }
        }
        gem.TriggerEvent("AttemptStartGame", gameObject, new List<object> { filteredSelection });
    }

    //Set what sprite num
    public void pickFast()
    {
        robberNum = 0;
    }
    public void pickStrong()
    {
        robberNum = 1;
    }
    public void pickBig()
    {
        robberNum = 2;
    }
    public void pickGreedy()
    {
        robberNum = 3;
    }

    //Set what button slot player chooses
    public void slot0()
    {
        slotNum = 0;
    }
    public void slot1()
    {
        slotNum = 1;
    }
    public void slot2()
    {
        slotNum = 2;
    }
    public void slot3()
    {
        slotNum = 3;
    }
}
