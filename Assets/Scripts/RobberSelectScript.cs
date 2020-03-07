using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobberSelectScript : MonoBehaviour
{
    private int slotNum = -1;
    private int robberNum = -1;
    public Sprite[] robberList;
    public Button[] buttonList;

    private Button button;
    private Sprite robberPortrait;

    // Update is called once per frame
    void Update()
    {
        //displays image over button
        if (robberNum > -1 && slotNum > -1)
        {
            robberPortrait = robberList[robberNum];
            button = buttonList[slotNum];
            button.image.sprite = robberPortrait;
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
