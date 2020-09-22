using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    
    private GameObject player;
    private StageManger stageManger;
    private CameraManger cameraManger;
    private InGameUIManger inGameManger;
    public EnumInfo.GameState gameState;


  

     private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

        }
        else
        {
            GameObject.Destroy(this);
        }
    }



    private void Start()
    {
        if (gameState == EnumInfo.GameState.Ingame)
        {
            print("DDS");
            getStageManger().getRoom(0).PlayerRoomIn();
        }
    }


    public void InGameSetting()
    {
        print("Player Setting...");
        SetPlayer();
        print("Player Finsh...");
   print("Stage Setting...");
        SetStageManger();
        print("Stage Finsh...");
        print("CameraManger Setting...");
        SetCameraManger();
        print("CameraManger Finsh...");
        print("InGameUIManger Setting...");
        SetInGameUIManger();
        print("InGameUIManger Finsh...");
    }




    public void SetPlayer()
    {
        player =  GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("No Serch Player " );
            return;
        }
      
    }

    public void SetCameraManger()
    {
        GameObject cameraMangerObj = GameObject.Find("CameraManger");
        if (cameraMangerObj == null)
        {
            Debug.LogError("No Serch cameraManger ");
            return;
        }
        cameraManger = cameraMangerObj.GetComponent<CameraManger>();
        cameraManger.Setting();
    }

    public void SetStageManger()
    {
        GameObject  stageMangerObj= GameObject.Find("StageManger");

        if (stageMangerObj == null)
        {
            Debug.LogError("No Serch StageManger ");
            return;
        }
            stageManger = stageMangerObj.GetComponent<StageManger>();
            stageManger.Setting();
    }

    public void SetInGameUIManger()
    {
        GameObject inGameMangerObj = GameObject.Find("InGameUIManger");
        if (inGameMangerObj == null)
        {
            Debug.LogError("No Serch inGameManger ");
            return;
        }
        inGameManger = inGameMangerObj.GetComponent<InGameUIManger>();
        inGameManger.Setting();
    }

    public GameObject getPlayerObject()
    {
        if (player == null) SetPlayer();
        return player;
    }

    public PlayerControl getPlayerControl()
    {
        if (player == null) SetPlayer();
        return player.GetComponent<PlayerControl>();
    }

    public StageManger getStageManger()
    {
        if (stageManger == null) SetStageManger();
        return stageManger;
    }
    
    public CameraManger getCameraManger()
    {
      //  if (cameraManger == null) SetCameraManger();
        return cameraManger;
    }

    public InGameUIManger getInGameUIMangerI()
    {
        if (inGameManger == null) SetInGameUIManger();
        return inGameManger;
    }

    public void SetGameState(EnumInfo.GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case EnumInfo.GameState.Ingame:
                Time.timeScale = 1;
                break;
            case EnumInfo.GameState.Pause:
            case EnumInfo.GameState.GameOver:
                Time.timeScale = 0;
                break;
        }

    }

    public EnumInfo.GameState GetGameState()
    {
        return gameState;
    }


    public void GoToTitleScene()
    {
        SceneManager.LoadScene(0);
        SetGameState(EnumInfo.GameState.Title);
    }

    public void GoToInGameScene()
    {
        SceneManager.LoadScene(1);
        SetGameState(EnumInfo.GameState.Ingame);
    }

    public void GameExit()
    {
        Application.Quit();
    }


    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 교체됨, 현재 씬: " + scene.name);
        if (scene.name == "MergeTestSenes")
        {
            // 씬 전환 효과 (Fade In)
            InGameSetting();
        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
