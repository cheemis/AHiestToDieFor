using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GlobalEventManager gem;

    private List<GameObject> robbers;

    public TextMeshProUGUI moneyText;
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
        robbers = new List<GameObject>();
    }

    private void Start()
    {
        gem.StartListening("UpdateMoney", UpdateMoney);
        gem.StartListening("EscapeWithMoney", Escape);
        gem.StartListening("RobberEnteredSpawnArea", TrackRobber);
        gem.StartListening("Death", RemoveRobber);
    }
    private void OnDestroy()
    {
        gem.StopListening("UpdateMoney", UpdateMoney);
        gem.StopListening("EscapeWithMoney", Escape);
        gem.StopListening("RobberEnteredSpawnArea", TrackRobber);
        gem.StopListening("Death", RemoveRobber);
    }

    private void TrackRobber(GameObject target, List<object> parameters)
    {
        if (robbers.Contains(target))
        {
            return;
        }
        robbers.Add(target);
    }
    private void RemoveRobber(GameObject target, List<object> parameters)
    {
        if (!robbers.Contains(target))
        {
            throw new Exception("Missing robber: Tried to remove robber that didn't exist");
        }
        robbers.Remove(target);
    }
    private float GetAccumulatedStolenMoney()
    {
        robbers = robbers.Where(robber => robber != null).ToList();
        return robbers.Select(robber => robber.GetComponent<MoneyBag>().money).Sum();
    }
    private void UpdateMoney(GameObject target, List<object> parameters)
    {
        moneyText.text = string.Format("Stolen money: ${0}", GetAccumulatedStolenMoney());
    }
    private void Escape(GameObject target, List<object> parameters)
    {
        if (parameters.Count == 0)
        {
            throw new Exception("Missing parameter: Could not find list of robbers parameter");
        }
        foreach(object robber in parameters)
        {
            if (robber.GetType() != typeof(GameObject))
            {
                throw new Exception("Illegal argument: parameter wrong type");
            }
        }
        List<GameObject> robbersCloseToEscapeVan = parameters.Select(robber => (GameObject)robber).ToList();
        if (robbers.All(robbersCloseToEscapeVan.Contains))
        {
            if (GetAccumulatedStolenMoney() >= 15000)
            {
                LoadNewScene.scene = SceneManager.GetActiveScene().buildIndex + 1;

                //load next scene unless no more levels, then load title screen
                if(LoadNewScene.scene <= 2) {SceneManager.LoadScene(LoadNewScene.scene);}
                else {SceneManager.LoadScene(0);}
            }
        }
    }
}
