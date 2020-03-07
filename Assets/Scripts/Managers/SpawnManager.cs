using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public List<GameObject> TEST_SPAWN_ROBBERS;

    private GlobalEventManager gem;
    private Queue<GameObject> robbersSpawnQueue;

    private int currentSpawnPoints = 0;
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
        robbersSpawnQueue = new Queue<GameObject>(TEST_SPAWN_ROBBERS); 
    }
    private void Start()
    {
        gem.StartListening("RobbersSelected", RobbersSelected);
        gem.StartListening("Death", SpawnNextRobber);
    }
    private void OnDestroy()
    {
        gem.StopListening("RobbersSelected", RobbersSelected);
        gem.StopListening("Death", SpawnNextRobber);
    }
    private void RobbersSelected(GameObject target, List<object> parameters)
    {
        if (parameters.Count == 0)
        {
            throw new Exception("Missing parameter: Could not find list of robbers");
        }
        if (parameters[0].GetType() != typeof(Queue<GameObject>))
        {
            throw new Exception("Illegal argument: parameter wrong type: " + parameters[0].GetType().ToString());
        }
        robbersSpawnQueue = (Queue<GameObject>) parameters[0];
        if (robbersSpawnQueue.Count < 2)
        {
            // TODO, lose game instantly
            gem.TriggerEvent("LostGame", gameObject);
            throw new Exception("Invalid parameters: No robbers in robber list");
        }
        InstantiateRobbers(Constants.MAX_ROBBERS_OUT_SIMULTANEOUSLY);
    }
    private void InstantiateRobbers(int amount)
    {
        if (robbersSpawnQueue.Count == 0)
        {
            gem.TriggerEvent("LostGame", gameObject);
            return;
        }
        if (amount > robbersSpawnQueue.Count)
        {
            throw new Exception("Illegal argument: trying to instantiate more robbers than exist in the spawn queue");
        }
        for (int i = 0; i < amount; i++)
        {
            Instantiate(robbersSpawnQueue.Dequeue(), spawnPoints[currentSpawnPoints % spawnPoints.Count].transform.position, spawnPoints[currentSpawnPoints % spawnPoints.Count].transform.rotation);
            currentSpawnPoints++;
        }
    }
    private void SpawnNextRobber(GameObject target, List<object> parameters)
    {
        InstantiateRobbers(1);
    }
}
