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
        findPlayer();
        if(Vector3.Distance(transform.position, patrolPoints[currentPoint % patrolPoints.Length]) < .1)
        {
            FindNewPoint();
        }
    }

    private void FindNewPoint()
    {
        currentPoint ++;
        agent.SetDestination(patrolPoints[currentPoint % patrolPoints.Length]);
    }

    private bool findPlayer()
    {
        RaycastHit hit;
        Ray ray = new Ray(viewPoint.transform.position, player.transform.position);
        Debug.DrawRay(viewPoint.transform.position,(player.transform.position - viewPoint.transform.position), Color.white, 0.0f, true);
        //float distance = (viewPoint.transform.position - player.transform.position).magnitude;

        if(Physics.Raycast(viewPoint.transform.position, (player.transform.position - viewPoint.transform.position), out hit))
        {
            if(hit.transform.gameObject.tag == "Player")
            {
                Debug.Log("found player");
                return true;
            }
            Debug.Log("can't find player: " + hit.transform.gameObject);
        }
        Debug.Log(hit);
        return false;
    }
}
