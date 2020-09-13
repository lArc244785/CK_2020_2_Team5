using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerInSensor : MonoBehaviour
{
    private Room room;

    private void Start()
    {
        room = GetComponentInParent<Room>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            room.PlayerRoomIn();
        }
    }
}
