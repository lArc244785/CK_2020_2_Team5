using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool isPlayerIn;
    private bool isRoomClear = false;
    private List<Door> doorList = new List<Door>();

    private MonsterGenerator mg;

    public Vector3 offsetSize;
    public Vector3 offsetPosition;
    private Vector2 offsetUpperLeft;
    private Vector2 offsetDownRight;

    public EnumInfo.CamType roomCamType;

    public Vector3 fixingPosition;

    private void Awake()
    {
        print("SCENE AWAKE");
        DoorListSetting();
        OffsetPointSetting();
        DoorClose();

        //이후는 몬스터 나오나 안 나오나 확인 후
        mg = transform.GetChild(0).GetComponent<MonsterGenerator>();
    }



    private void DoorListSetting()
    {
        Transform doorListTr = transform.Find("DoorList").transform;

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



    private void OffsetPointSetting()
    {
        float halfX = offsetSize.x / 2;
        float halfZ = offsetSize.z / 2;

        Vector3 offsetPos = transform.position - offsetPosition;

        offsetUpperLeft.x = offsetPos.x - halfX;
        offsetUpperLeft.y = offsetPos.z + halfZ;

        offsetDownRight.x = offsetPos.x + halfX;
        offsetDownRight.y = offsetPos.z - halfZ;
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
        if (isPlayerIn == true) return;
        Debug.Log("PlayerRoomIn" + " GameObject" + gameObject.name);
        GameManger.instance.getCameraManger().SetOffset(offsetUpperLeft, offsetDownRight);
        mg.MonsterEventOn();
        isPlayerIn = true;
        GameManger.instance.getCameraManger().setCamType(roomCamType);
        if(roomCamType == EnumInfo.CamType.Fixing)
        {
            GameManger.instance.getCameraManger().SetFixingCameraPoint(fixingPosition);
        }
    }

    public void PlayerRoomOut()
    {
        if (isPlayerIn == false) return;
        Debug.Log("PlayerRoomOut" + " GameObject" + gameObject.name);
        mg.MonsterEventOff();
        isPlayerIn = false;
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        PlayerRoomIn();
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position - offsetPosition, offsetSize);
    }

    
    
}
