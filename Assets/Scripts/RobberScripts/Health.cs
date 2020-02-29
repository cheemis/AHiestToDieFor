using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private GlobalEventManager gem;
    private Animator animator;
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
        animator = GetComponent<Animator>();
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
            //instantiate bag of money
            StartCoroutine("FallingOver");
        }
    }

    private IEnumerator FallingOver()
    {
        animator.SetBool("isDead",true);
        yield return new WaitForSeconds(3);
        gem.TriggerEvent("Death", gameObject);
        Destroy(gameObject);
    }
}
