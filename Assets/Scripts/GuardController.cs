using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GuardController : MonoBehaviour
{
    //nav mesh stuff
    public NavMeshAgent agent;
    // public Vector3[] patrolPoints;
    // private int currentPoint = 0;

    //references
    public GameObject player;
    public GameObject viewPoint;

    //guard view info
    public float viewDistance = 20;
    public float fov = 60;
    private Vector3 lastPointSeen;

    //guard mode info
    private string action = "idle";

    //Gun stuff
    public GameObject bullet;
    public GameObject gunPoint;

    //coroutine stuff
    private bool waitCoOn = false;
    private Coroutine Co;
    public int waitingTime = 5;

    // Start is called before the first frame update
    void Start()
    {   
    }

    public void ParentStart()
    {
        //get components
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(transform.position);

        //instantiate world
        player = GameObject.FindWithTag("Player");
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void findPlayer()
    {
        player = GameObject.FindWithTag("Player");

        //if the player is within viewing distance of teh guard
        if(Vector3.Distance(player.transform.position, transform.position) < viewDistance)
        {
            //if the player is in front of the guard
            if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov/ 2f)
            {
                //Create Raycast
                RaycastHit hit;
                Ray ray = new Ray(viewPoint.transform.position, player.transform.position);

                //see if the raycast hits something
                if(Physics.Raycast(viewPoint.transform.position, (player.transform.position - viewPoint.transform.position), out hit) && hit.transform.CompareTag("Player"))
                {
                    //if the object is tagged the player
                    if(hit.transform.gameObject.tag == "Player")
                    {
                        //replace with chase the player
                        Debug.Log("found player");
                        action = "attack";
                    }
                }
            }
        }
    }

    public void Chase()
    {
        StopCoroutine("Reloading");
        StopCoroutine("idle");
        waitCoOn = false;

        RaycastHit hit;

        //if can directly see player, attack case
        if(player &&
           Physics.Raycast(viewPoint.transform.position, (player.transform.position - viewPoint.transform.position), out hit) &&
           hit.transform.CompareTag("Player") &&
           Vector3.Distance(player.transform.position, transform.position) < viewDistance)
        {
            action = "attack";
        }
        else if(Vector3.Distance(lastPointSeen, transform.position) > .1)
        {
            agent.SetDestination(lastPointSeen);
        }
        else
        {
            action = "idle";
            player = null;
        }
    }

    public void Attack()
    {
        agent.SetDestination(transform.position);

        if(Quaternion.Angle(Quaternion.LookRotation(player.transform.position - transform.position), transform.rotation) > 5 &&
           Vector3.Distance(player.transform.position, transform.position) < viewDistance)
        {
            //look towards player
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            float strength = Mathf.Min(10 * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);
        }
        //attack player
        else if(Vector3.Distance(player.transform.position, transform.position) < viewDistance)
        {
            //shoot at player
            if (!waitCoOn)
            {
                Co = StartCoroutine("Reloading");
            }
            Debug.Log("shooting at player");
        }
        //can't see player or facing player
        else
        {
            action = "chase";
        }

        lastPointSeen = player.transform.position;
    }

    public void Idle()
    {
        if (!waitCoOn)
        {
            Co = StartCoroutine("Waiting");
        }
    }

    IEnumerator Waiting()
    {
        waitCoOn = true;
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(waitingTime);
        action = "guard";
        waitCoOn = false;
    }

    IEnumerator Reloading()
    {
        waitCoOn = true;
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, gunPoint.transform.position, gunPoint.transform.rotation);
        waitCoOn = false;
    }

    public void SetAction(string newAction)
    {
        action = newAction;
    }

    public string GetAction() {return action;}

    public void SetWaitCoOn(bool value) {waitCoOn = value;}

}
