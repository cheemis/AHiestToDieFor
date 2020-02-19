using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private GlobalEventManager gem;

    public int maxHealth;
    private int health;

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

        this.health = maxHealth;
    }

    void Start()
    {
        gem.StartListening("Shot", TakeDamage);
    }

    public void OnDestroy()
    {
        gem.StopListening("Shot", TakeDamage);
    }

    public void TakeDamage(GameObject target, List<object> parameters)
    {
        if (target != gameObject)
        {
            return;
        }

        health -= 1;
        if (health <= 0)
        {
            gem.TriggerEvent("Death", gameObject);
            Destroy(gameObject);
        }
    }
}
