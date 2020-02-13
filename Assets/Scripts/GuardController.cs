using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3[] patrolPoints;
    public GameObject player;
    public GameObject viewPoint;
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
        //Look for the player, otherwise, patrol
        findPlayer();
        if(Vector3.Distance(transform.position, patrolPoints[currentPoint % patrolPoints.Length]) < .1)
        {
            FindNewPoint();
        }
    }

    private void FindNewPoint()
    {
        //This function sets a new destination fot the guard
        currentPoint ++;
        agent.SetDestination(patrolPoints[currentPoint % (patrolPoints.Length - 1)]);
    }

    private bool findPlayer()
    {
        //Create Raycast
        RaycastHit hit;
        Ray ray = new Ray(viewPoint.transform.position, player.transform.position);
        
        //Visualize raycast in Scene play. Doesn't effect gameplay
        Debug.DrawRay(viewPoint.transform.position,(player.transform.position - viewPoint.transform.position), Color.white, 0.0f, true);

        //see if the raycast hits something
        if(Physics.Raycast(viewPoint.transform.position, (player.transform.position - viewPoint.transform.position), out hit))
        {
            //if the object is tagged the player
            if(hit.transform.gameObject.tag == "Player")
            {
                //replace with chase the player
                Debug.Log("found player");
                return true;
            }
            else
            {
                //replace with can't find player
                Debug.Log("can't find player: " + hit.transform.gameObject);
            }
        }
        Debug.Log(hit);
        return false;
    }
}
