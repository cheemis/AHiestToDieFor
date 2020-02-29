using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    private GlobalEventManager gem;
    private NavMeshAgent agent;
    private Animator animator;
    public float speed;
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

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat("velocity", agent.velocity.magnitude * agent.speed);
    }

    private void Start()
    {
        gem.StartListening("Move", Move);
        animator = GetComponent<Animator>();
        //animator.speed = agent.speed/2f;
    }
    private void OnDestroy()
    {
        gem.StopListening("Move", Move);
    }

    private void Move(GameObject target, List<object> parameters)
    {
        if (target != gameObject)
        {
            return;
        }
        if (parameters.Count == 0)
        {
            throw new Exception("Missing parameter: Could not find target location of movement");
        }
        if (parameters[0].GetType() != typeof(Vector3))
        {
            throw new Exception("Illegal argument: parameter wrong type");
        }

        Vector3 location = (Vector3) parameters[0];// as Vector3;

        agent.SetDestination(location);
        agent.speed = speed;
    }
}
