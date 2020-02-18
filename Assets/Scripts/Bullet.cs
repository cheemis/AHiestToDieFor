using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private bool collided = false;

    //components
    private Rigidbody rb;
    private MeshRenderer mr;

    private float startTime;
    public float lifeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > lifeTime)
        {
            collided = true;
        }
        print(Time.time - startTime);

        if(collided)
        {
            mr.enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        print("hit something");
        collided = true;
    }

    void FixedUpdate()
    {
        if(!collided)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
