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

    private Dictionary<GameObject, string> robberToStolenMap;
    private bool vaultStolen;
    public List<GameObject> robbers;

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
        robberToStolenMap = new Dictionary<GameObject, string>();
    }

    private void Start()
    {
        gem.StartListening("StoleVault", VaultStolen);
        gem.StartListening("UpdateRobbers", UpdateRobbers);
        gem.StartListening("UpdateMoney", UpdateMoney);
        gem.StartListening("Exit", WinGame);
    }
    private void OnDestroy()
    {
        gem.StopListening("StoleVault", VaultStolen);
        gem.StopListening("UpdateRobbers", UpdateRobbers);
        gem.StopListening("UpdateMoney", UpdateMoney);
        gem.StopListening("Exit", WinGame);
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
    private void UpdateRobbers(GameObject target, List<object> parameters)
    {
        robbers = parameters
            .Select(robber => (GameObject)robber)
            .ToList();
    }
    private void VaultStolen(GameObject target, List<object> parameters)
    {
        robberToStolenMap[target] = "Vault";
    }
    private void WinGame(GameObject target, List<object> parameters)
    {
        if (robberToStolenMap.ContainsKey(target) && robberToStolenMap[target] == "Vault")
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
