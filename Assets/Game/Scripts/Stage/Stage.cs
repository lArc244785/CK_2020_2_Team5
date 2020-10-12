using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private bool isPlayerIn;
    private bool isStageClear = false;
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
        //print("SCENE AWAKE");
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
        foreach (Door door in doorList)
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
        isStageClear = true;
        DoorOpen();
    }

    public void PlayerStageIn(Vector3 playerPostion, Vector3 playerRotaion, bool isPass = false)
    {
        if (isPlayerIn == true) return;
        StartCoroutine(StageLoadEvent(isPass, playerPostion, playerRotaion));
    }


    private IEnumerator StageLoadEvent(bool isPass, Vector3 playerPostion, Vector3 playerRotaion)
    {
        Debug.Log("CODE 7984:  StageLoadEvnetOn");

        if (!isPass)
        {
            Debug.Log("CODE 7984:  StageLoadEvnet noPass");
            GameManger.instance.SetGameState(EnumInfo.GameState.Loading);



            UI_LoadingView loadEvent = GameManger.instance.GetLoadingView();
            loadEvent.PadeIn();
            yield return new WaitForSeconds(0.25f);

            Debug.Log("PlayerRoomIn" + " GameObject" + gameObject.name);
            GameManger.instance.getCameraManger().SetOffset(offsetUpperLeft, offsetDownRight);
            isPlayerIn = true;


            PlayerControl pc = GameManger.instance.getPlayerControl();
            //HP 1 회복
            pc.AddHP(1);

            Transform palyerTr = pc.transform;


            if (playerPostion != Vector3.zero)
                palyerTr.position = playerPostion;

            palyerTr.rotation = Quaternion.Euler(playerRotaion);

           



            GameManger.instance.getCameraManger().setCamType(roomCamType);
            GameManger.instance.getCameraManger().SetCamToTargetPos();
            if (roomCamType == EnumInfo.CamType.Fixing)
            {
                GameManger.instance.getCameraManger().SetFixingCameraPoint(fixingPosition);
            }
            loadEvent.PadeOut();
            yield return new WaitForSeconds(0.25f);
            loadEvent.SetLoadingViewActive(false);
            GameManger.instance.SetGameState(EnumInfo.GameState.Ingame);

            mg.MonsterEventOn();

        }
        else
        {
            Debug.Log("CODE 7984:  StageLoadEvnet Pass");
            Debug.Log("PlayerRoomIn" + " GameObject" + gameObject.name);

            if (playerPostion != Vector3.zero)
                GameManger.instance.getPlayerControl().transform.position = playerPostion;

            GameManger.instance.getCameraManger().SetOffset(offsetUpperLeft, offsetDownRight);
            mg.MonsterEventOn();
            isPlayerIn = true;
            GameManger.instance.getCameraManger().setCamType(roomCamType);
            if (roomCamType == EnumInfo.CamType.Fixing)
            {
                GameManger.instance.getCameraManger().SetFixingCameraPoint(fixingPosition);
            }



        }

        yield return null;
    }


    public void PlayerStageOut()
    {
        if (isPlayerIn == false) return;
        Debug.Log("PlayerRoomOut" + " GameObject" + gameObject.name);
        mg.MonsterEventOff();
        isPlayerIn = false;
    }

    private void Update()
    {
        if (roomCamType == EnumInfo.CamType.Fixing)
        {
            GameManger.instance.getCameraManger().SetFixingCameraPoint(fixingPosition);
        }
    }
    //-0.76 -18.33
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
