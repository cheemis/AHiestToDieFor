using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlate : MonoBehaviour
{
    public float distanceDown = .5f;

    public GameObject door;
    private DoorController doorController;

    public float doorDistanceX = 0;
    public float doorDistanceY = 0;
    public float doorDistanceZ = 0;

    public bool isMoving = false;

    private Vector3 movePlate;
    private Vector3 moveDoor;
    // Start is called before the first frame update
    void Start()
    {
        doorController = door.GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            Vector3.Lerp(door.transform.position, new Vector3(-doorDistanceX, -doorDistanceY, -doorDistanceZ), .2f);
        }   
    }
    //Move plate back up upon leaving and close the door
    private void OnTriggerExit(Collider other)
    {
        movePlate = new Vector3(0, distanceDown, 0);
        //moveDoor = new Vector3(doorDistanceX, doorDistanceY, doorDistanceZ);
        doorController.isPushed = false;

        this.gameObject.transform.Translate(movePlate);
        door.transform.Translate(moveDoor);
    }

    //Move plate down upon entering and open the door
    private void OnTriggerEnter(Collider other)
    {
        movePlate = new Vector3(0, -distanceDown, 0);
        doorController.isPushed = true;

        this.gameObject.transform.Translate(movePlate);
        door.transform.Translate(moveDoor);
    }
}
