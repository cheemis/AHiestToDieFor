using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private GlobalEventManager gem;

    private Dictionary<GameObject, string> robberToStolenMap;
    private bool vaultStolen;

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
        gem.StartListening("Exit", WinGame);
    }
    private void OnDestroy()
    {
        gem.StopListening("StoleVault", VaultStolen);
        gem.StopListening("Exit", WinGame);
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
