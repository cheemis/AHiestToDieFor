using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3[] patrolPoints;
    public GameObject player;
    private int currentPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindNewPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,patrolPoints[currentPoint]) < .1)
        {
            Debug.Log(currentPoint);
            FindNewPoint();
        }
    }

    private void FindNewPoint()
    {
        currentPoint ++;
        agent.SetDestination(patrolPoints[currentPoint % patrolPoints.Length]);
    }

    // private bool findPlayer()
    // {
    //     RaycastHit hit;
    //     Ray ray = new Ray(viewPoint.transform.position, player.transform.position);
    //     //float distance = (viewPoint.transform.position - player.transform.position).magnitude;

    //     if(Physics.Raycast(ray, out hit))
    //     {
    //         if(hit.transform.gameObject.tag == "Player")
    //         {
    //             Debug.Log("found player");
    //             return true;
    //         }
    //         Debug.Log("can't find player: " + hit.transform.gameObject);
    //     }
    //     Debug.Log(hit);
    //     return false;
    // }
}
