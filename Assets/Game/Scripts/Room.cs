using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool isPlayerIn;
    private bool isRoomClear = false;
    private List<Door> doorList = new List<Door>();
    private MonsterGenerator mg;

    private void Start()
    {
        DoorListSetting();
        DoorClose();
        mg = transform.GetChild(0).GetComponent<MonsterGenerator>();
        //이후는 몬스터 나오나 안 나오나 확인 후
    }


    private void DoorListSetting()
    {
        Transform doorListTr = transform.FindChild("DoorList").transform;

        for (int i = 0; i < doorListTr.childCount; i++)
        {
            try
            {
                Door tempDoor = doorListTr.GetChild(i).GetComponent<Door>();
                Debug.Log(tempDoor.name);
                doorList.Add(tempDoor);
            }
            catch
            {
                Debug.LogError("Room DoorSetting... No Serch Door.cs... Index " + i);
                return;
            }
        }
    }

    

    public void DoorClose()
    {
        foreach(Door door in doorList)
        {
            door.CloseDoor();
        }
    }

    public void DoorOpen()
    {
        foreach (Door door in doorList)
        {
            door.OpenderDoor();
        }
    } 

    public void RoomClear()
    {
        Debug.Log("Player RoomClear");
        isRoomClear = true;
        DoorOpen();
    }

    public void PlayerRoomIn()
    {
        Debug.Log("PlayerRoomIn" + " GameObject" + gameObject.name);
        isPlayerIn = true;
    }

    public void PlayerRoomOut()
    {
        Debug.Log("PlayerRoomOut" + " GameObject" + gameObject.name);
    }

    //private void Update()
    //{
    //    //플레이어가 들어와야 전체적인 룸 시스템이 돌아간다.
    //    if (isPlayerIn)
    //    {

    //    }
    //}

    public bool getIsPlayer()
    {
        return isPlayerIn;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerRoomIn();
        }
    }

}
