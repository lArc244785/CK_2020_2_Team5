using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerInSensor : MonoBehaviour
{
    private Stage room;

    private void Start()
    {
        room = GetComponentInParent<Stage>();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        room.PlayerStageIn();
    //    }
    //}
}
