﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlate : MonoBehaviour
{
    public float distanceDown = .5f;

    //keeps track if multiple robbers inside button collider
    private float numRobbersInside = 0;

    //We will call door scripts
    public GameObject door;
    public Door doorScript;

    private Vector3 movePlate;
    // Start is called before the first frame update
    void Start()
    {
        doorScript = door.GetComponent<Door>();
    }

    //Move plate back up upon leaving and close the door
    private void OnTriggerExit(Collider other)
    {
        if (numRobbersInside == 1)
        {
            movePlate = new Vector3(0, distanceDown, 0);
            doorScript.closeDoor();
            this.gameObject.transform.Translate(movePlate);
        }
        numRobbersInside--;
    }

    //Move plate down upon entering and open the door
    private void OnTriggerEnter(Collider other)
    {
        if (numRobbersInside == 0)
        {
            movePlate = new Vector3(0, -distanceDown, 0);
            doorScript.openDoor();
            this.gameObject.transform.Translate(movePlate);
        }
        numRobbersInside++;
    }
}
