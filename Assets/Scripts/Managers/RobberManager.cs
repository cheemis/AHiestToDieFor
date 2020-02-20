using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberManager : MonoBehaviour
{
    private GlobalEventManager gem;

    private void Awake()
    {
        List<MonoBehaviour> deps = new List<MonoBehaviour>
        {
            (gem = FindObjectOfType(typeof(GlobalEventManager)) as GlobalEventManager),
        };
        if (deps.Contains(null))
        {
            //throw new Exception("Could not find dependency");
        }
    }

    private void Start()
    {
        //gem.StartListening("Death", SpawnNextRobber);
    }

    private void OnDestroy()
    {
        //gem.StopListening("Death", SpawnNextRobber);
    }

    private void Death(GameObject target, List<object> parameters)
    {

    }
}
