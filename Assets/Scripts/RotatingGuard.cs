using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class RotatingGuard : GuardController
{
    public int numRotations = 4;
    private int currentView = 0;
    public Vector3[] views;
    private Vector3 rotation = Vector3.zero;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        ParentStart();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Visualize raycast in Scene play. Doesn't affect gameplay
        Debug.DrawRay(viewPoint.transform.position,(GameObject.FindWithTag("Player").transform.position - viewPoint.transform.position), Color.white, 0.0f, true);


        switch (GetAction())
        {
            case "idle":
                findPlayer();
                //stand and wait
                Idle();
                break;

            case "attack":
                //attack the player
                Attack();
                break;

            case "guard":
                findPlayer();
                //walk to next destination point
                Guard();
                break;
            
            case "chase":
                //chase the player
                StopCoroutine("Rotating");
                Chase();
                break;

            case "return":
                findPlayer();
                //return to patrol area
                Return();
                break;

            default:
                //error, action wasn't right
                StopCoroutine("Rotating");
                SetAction("idle");
                break;
        }
    }

    private void Guard()
    {
        if(rotation == Vector3.zero)
        {
            rotation = new Vector3(transform.position.x + views[currentView].x, 
                                   transform.position.y,
                                   transform.position.z + views[currentView].z);
        }
        if(!RotateTowards(transform.position, rotation))
        {
            rotation = Vector3.zero;
            currentView ++;
            if(currentView % views.Length == 0) {currentView = 0;}
            SetAction("idle");
        }
    }

    private void Return()
    {
        if(transform.position != origin)
        {
            agent.SetDestination(origin);
        }
        else
        {
            if(!RotateTowards(transform.position, new Vector3(origin.x, origin.y, origin.z + 1)))
            {
                SetAction("idle");
                currentView = 0;
            }
        }
    }

    private bool RotateTowards(Vector3 start, Vector3 end)
    {
        if(Quaternion.Angle(Quaternion.LookRotation(end - start), transform.rotation) != 0)
        {
            //look towards point
            Quaternion targetRotation = Quaternion.LookRotation(end - start);
            float strength = Mathf.Min(3 * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);
            return true;
        }
        return false;
    }

    IEnumerator Waiting()
    {
        SetWaitCoOn(true);
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(waitingTime);

        if(transform.position != origin) {SetAction("return");}
        else{SetAction("guard");}

        SetWaitCoOn(false);
    }
}
