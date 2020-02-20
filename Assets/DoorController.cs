using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isPushed;

    public Vector3 opened;
    private Vector3 closed;

    // Start is called before the first frame update
    void Start()
    {
        closed = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPushed)
        {
            transform.position = Vector3.Lerp(transform.position, opened, .01f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closed, .01f);
        }
    }
}
