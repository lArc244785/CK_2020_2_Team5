using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManger : MonoBehaviour
{
    private List<Room> roomList = new List<Room>();

    private void Awake()
    {
        Setting();
    }

    private void Setting()
    {
        Transform tr = transform;
        for (int i = 0; i < tr.GetChildCount(); i++)
        {
            try
            {
                Room tempRoom = tr.GetChild(i).GetComponent<Room>();
                Debug.Log(tempRoom.gameObject);
                roomList.Add(tempRoom);
            }
            catch 
            {
                Debug.LogError("StageManger_RoomSettting Error: " + i);
                return;
            }
         
        }
    }
}
