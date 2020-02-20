using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{

    public Vector3[] rooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveCamera()
    {
        transform.position = Vector3.Slerp(transform.position, rooms[0], .1f);
    }
}
