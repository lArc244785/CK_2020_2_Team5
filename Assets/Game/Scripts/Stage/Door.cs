using UnityEngine;

public class Door : MonoBehaviour
{
    private Stage room;

    public Door nextDoor;

    private bool isOpen = false;

    private float pointX;

    private float offsetPoint = 0.5f;
    public EnumInfo.DoorSpawn SpawnPoint;

    private Vector3 movePoint;
    private Vector3 rotion;

    private Animator ani;

    public void Start()
    {
        room = transform.GetComponentInParent<Stage>();
        PointSetting();
        if (ani == null)
        {
            ani = transform.FindChild("Model").GetComponent<Animator>();
        }
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
                rotion = new Vector3(0, 270, 0);
                break;
            case EnumInfo.DoorSpawn.Right:
                movePoint = new Vector3(
                    transform.position.x + offsetPoint,
                    0.0f,
                    transform.position.z);
                rotion = new Vector3(0, 90, 0); 
                break;
            case EnumInfo.DoorSpawn.Up:
                movePoint = new Vector3(
                    transform.position.x ,
                    0.0f,
                    transform.position.z + offsetPoint);
                rotion = new Vector3(0, 0, 0);
                break;
            case EnumInfo.DoorSpawn.Down:
                movePoint = new Vector3(
                    transform.position.x,
                    0.0f,
                    transform.position.z - offsetPoint);
                rotion = new Vector3(0, 180, 0);
                break;
        }


    }

    private void OppenedTheDoor()
    {
        if (isOpen)
        {
            Debug.Log("Next  " + nextDoor.room.gameObject);
            nextDoor.RoomMove(GameManger.instance.getPlayerObject());
            room.PlayerStageOut();

        }
    }


    public void RoomMove(GameObject target)
    {
        movePoint.y = target.transform.position.y;
        //target.transform.position = movePoint;
        room.PlayerStageIn(movePoint, rotion);
    }

    public void CloseDoor()
    {
        isOpen = false;
        if(ani == null)
        {
            ani = transform.FindChild("Model").GetComponent<Animator>();
        }
        ani.SetBool("IsOpen", isOpen);
    }
    public void OpenderDoor()
    {
        isOpen = true;
        if (ani == null)
        {
            ani = transform.FindChild("Model").GetComponent<Animator>();
        }
        ani.SetBool("IsOpen", isOpen);
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            OppenedTheDoor();
        }
    }
}
