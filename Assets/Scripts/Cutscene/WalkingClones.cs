using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingClones : MonoBehaviour
{
    public float speed = 1;
    private float spawn = -90;
    private float bound = -117.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > bound)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(spawn, transform.position.y, transform.position.z);
        }
    }
}
