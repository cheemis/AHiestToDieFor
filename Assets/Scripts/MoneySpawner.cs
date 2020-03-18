using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneySpawner : MonoBehaviour
{
    public GameObject stackOfMoney;
    public GameObject textObject;
    private TMP_Text text;

    public GameObject button;

    public float speed = 1f;
    private float speedup = 0;
    public float speedupAmount = 1f;
    private float countingMoney = 0;
    private bool doneCountingMoney = false;
    private float addingAmount = 0;
    private bool WaitCoOn = false;




    // Start is called before the first frame update
    void Start()
    {
        text = textObject.GetComponent<TMP_Text>();
        for(int i = 0; i < 200; i ++)
        {
            StartCoroutine("StartMoney");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!doneCountingMoney)
        {
            WriteMoney();
            if(!WaitCoOn) {StartCoroutine("SpawnMoney");}
            speed = Mathf.Min(.5f, 100/(countingMoney * countingMoney * countingMoney * countingMoney));
        }
        else
        {
            button.SetActive(true);
        }
    }

    private void WriteMoney()
    {
        countingMoney += (int) (addingAmount + speedup);

        if(countingMoney > StaticMoney.GetTotalMoney())
        {
            doneCountingMoney = true;

            countingMoney = (int) StaticMoney.GetTotalMoney();

            // sets money to total money to prepare for next map
            StaticMoney.SetMoney(StaticMoney.GetTotalMoney());

            //button.SetActive(true);
        }

        text.text = "Total: " + countingMoney;

        speedup += speedupAmount;
    }


    IEnumerator StartMoney()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(stackOfMoney,
            new Vector3(Random.Range(-1.5f,1.5f), 10, Random.Range(-.5f,.5f)),
            Quaternion.Euler(Random.Range(0f,360f), Random.Range(0f,360f), Random.Range(0f,360f)));
    }

    IEnumerator SpawnMoney()
    {
        WaitCoOn = true;
        yield return new WaitForSeconds(speed);
        Instantiate(stackOfMoney,
                    new Vector3(Random.Range(-.5f,.5f), 10, Random.Range(-.5f,.5f)),
                    Quaternion.Euler(Random.Range(0f,360f), Random.Range(0f,360f), Random.Range(0f,360f)));
        WaitCoOn = false;
    }
}
