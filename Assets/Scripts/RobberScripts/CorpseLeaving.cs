using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CorpseLeaving : MonoBehaviour
{
    private GlobalEventManager gem;

    public GameObject corpse;

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
        gem.StartListening("Death", LeaveCorpseBehind);
    }
    private void OnDestroy()
    {
        gem.StopListening("Death", LeaveCorpseBehind);
    }

    private void LeaveCorpseBehind(GameObject target, List<object> parameters)
    {
        if (target != gameObject)
        {
            return;
        }

        //To be added, add CorpseManager tracking number of dead robbers and handling any further logic surrounding corpses.

        Instantiate(corpse, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
