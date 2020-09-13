using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    
    private GameObject player;
    private StageManger stageManger;
    private CameraManger cameraManger;
    private UIManger uiManger;

    private EnumInfo.GameState gameState;

     private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            Setting();
            DontDestroyOnLoad(this);

            //임시
            gameState = EnumInfo.GameState.Ingame;
        }
        else
        {
            GameObject.Destroy(this);
        }
    }

    private void Start()
    {
      stageManger.getRoom(0).PlayerRoomIn();
    }


    private void Setting()
    {
        SetPlayer();
        SetStageManger();
        SetCameraManger();
        SetUIManger();

        uiManger.Setting();
       
    }

    public void SetPlayer()
    {
        player =  GameObject.Find("Player");
        Debug.Log("Player " + player.name);
    }

    public void SetCameraManger()
    {
        cameraManger = GameObject.Find("CameraManger").GetComponent<CameraManger>();
    }

    public void SetStageManger()
    {
        stageManger = GameObject.Find("StageManger").GetComponent<StageManger>();
    }

    public void SetUIManger()
    {
        uiManger = GameObject.Find("UIManger").GetComponent<UIManger>();
    }

    public GameObject getPlayerObject()
    {
        return player;
    }

    public PlayerControl getPlayerControl()
    {
        return player.GetComponent<PlayerControl>();
    }

    public StageManger getStageManger()
    {
        return stageManger;
    }
    
    public CameraManger getCameraManger()
    {
        return cameraManger;
    }

    public UIManger getUIMangerI()
    {
        return uiManger;
    }

    public void SetGameState(EnumInfo.GameState state)
    {
        gameState = state;
    }

    public EnumInfo.GameState GetGameState()
    {
        return gameState;
    }

}
