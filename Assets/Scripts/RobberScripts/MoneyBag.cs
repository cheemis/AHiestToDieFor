using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyBag : MonoBehaviour
{
    private GlobalEventManager gem;

    public float money;
    public GameObject keyCard;

    public GameObject backpack;
    public GameObject backpackPrefab;
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
        money = 0;
    }

    private void Start()
    {
        gem.StartListening("AddMoneyToRobber", AddMoney);
        gem.StartListening("KeyCardStolen", KeyCardStolen);
        gem.StartListening("Death", DropMoney);
    }
    private void OnDestroy()
    {
        gem.StopListening("AddMoneyToRobber", AddMoney);
        gem.StopListening("KeyCardStolen", KeyCardStolen);
        gem.StopListening("Death", DropMoney);
    }

    private void DropMoney(GameObject target, List<object> parameters)
    {
        if (target != gameObject)
        {
            return;
        }
        backpack.SetActive(false);
        GameObject clone = Instantiate(backpackPrefab, backpack.transform.position, backpack.transform.rotation);
        MoneyNotifyer script = clone.GetComponent<MoneyNotifyer>();
        if (script == null)
        {
            throw new Exception("Did not find MoneyNotifyer on backpack prefab");
        }
        script.amount = money;
        money = 0;
        gem.TriggerEvent("UpdateMoney", gameObject);
    }
    private void AddMoney(GameObject target, List<object> parameters)
    {
        if (target != gameObject)
        {
            return;
        }
        if (parameters.Count == 0)
        {
            throw new Exception("Missing parameter: Could not find amount of money to add");
        }
        if (parameters[0].GetType() != typeof(float))
        {
            throw new Exception("Illegal argument: parameter wrong type");
        }

        money += (float) parameters[0];
        gem.TriggerEvent("UpdateMoney", gameObject);
    }

    private void KeyCardStolen(GameObject target, List<object> parameters)
    {
        if (target != gameObject)
        {
            return;
        }
        if (parameters.Count == 0)
        {
            throw new Exception("Missing parameter: Could not find keycard to add");
        }
        if (parameters[0].GetType() != typeof(GameObject))
        {
            throw new Exception("Illegal argument: parameter wrong type");
        }

        keyCard = (GameObject)parameters[0];
    }
}
