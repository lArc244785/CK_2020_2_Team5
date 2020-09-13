using UnityEngine;

public class Door : MonoBehaviour
{
    private Room room;

    public Door nextDoor;

    private bool isOpen = false;

    private float pointX;

    private float offsetPoint = 1.5f;
    public EnumInfo.DoorSpawn SpawnPoint;

    private Vector3 movePoint;

    public void Start()
    {
        room = transform.GetComponentInParent<Room>();
        PointSetting();
    }

    public void PointSetting()
    {
        pointX = transform.position.x;

        switch (SpawnPoint)
        {
            case EnumInfo.DoorSpawn.Left:
                movePoint = new Vector3(
                    transform.position.x - offsetPoint,
                    0.0f,
                    transform.position.z);
                break;
            case EnumInfo.DoorSpawn.Right:
                movePoint = new Vector3(
                    transform.position.x + offsetPoint,
                    0.0f,
                    transform.position.z);
                break;
            case EnumInfo.DoorSpawn.Up:
                movePoint = new Vector3(
                    transform.position.x ,
                    0.0f,
                    transform.position.z + offsetPoint);
                break;
            case EnumInfo.DoorSpawn.Down:
                movePoint = new Vector3(
                    transform.position.x,
                    0.0f,
                    transform.position.z - offsetPoint);
                break;
        }


    }

    private void OppenedTheDoor()
    {
        if (isOpen)
        {
            Debug.Log("Next  " + nextDoor.room.gameObject);
            nextDoor.RoomMove(GameManger.instance.getPlayerObject());
            room.PlayerRoomOut();

        }
    }


    public void RoomMove(GameObject target)
    {
        movePoint.y = target.transform.position.y;
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

        if (collider.gameObject.tag == "Player")
        {
            OppenedTheDoor();
        }
    }
}
