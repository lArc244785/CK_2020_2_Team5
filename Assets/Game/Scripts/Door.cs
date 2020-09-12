using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Room room;

    public Door nextDoor;

    private bool isOpen = false;

    private float pointX;

    private float offsetX = 1.5f ;
    public bool isRight;

    public void Start()
    {
        room = transform.GetComponentInParent<Room>();
        PointSetting();
    }

    public void PointSetting()
    {
        pointX = transform.position.x;
        if (isRight)
        {
            pointX += offsetX;
        }
        else
        {
            pointX -= offsetX;
        }
    }

    private void OppenedTheDoor()
    {
        if (isOpen)
        {
            Debug.Log("Next  " + nextDoor.room.gameObject);
            nextDoor.RoomMove(GameManger.instance.getPlayer());
            room.PlayerRoomOut();

        }
    }


    public void RoomMove(GameObject target)
    {   
        Vector3 movePoint = new Vector3(pointX, target.transform.position.y, transform.position.z);
        print("Next MovePoint: " + movePoint);
        target.transform.position = movePoint;
        room.PlayerRoomIn();
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
    public void OpenderDoor()
    {
        isOpen = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        if(collider.gameObject.tag == "Player")
        {
           OppenedTheDoor();
        }
    }
}
