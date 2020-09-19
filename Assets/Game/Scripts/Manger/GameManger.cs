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

            if(gameState == EnumInfo.GameState.Ingame)
            {
                InGameSetting();

                SetGameState(EnumInfo.GameState.Ingame);
            }
      

        }
        else
        {
            GameObject.Destroy(this);
        }
    }

    private void Start()
    {
        if(gameState == EnumInfo.GameState.Ingame)
            getStageManger().getRoom(0).PlayerRoomIn();
    }


    public void InGameSetting()
    {
        SetPlayer();
        SetStageManger();
        SetCameraManger();
        SetInGameUIManger();

        cameraManger.Setting();
        inGameManger.Setting();
        stageManger.Setting();

      

    }




    public void SetPlayer()
    {
        player =  GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("No Serch Player " );
        }
      
    }

    public void SetCameraManger()
    {
        cameraManger = GameObject.Find("CameraManger").GetComponent<CameraManger>();
    }

    public void SetStageManger()
    {
        stageManger = GameObject.Find("StageManger").GetComponent<StageManger>();
    }

    public void SetInGameUIManger()
    {
        inGameManger = GameObject.Find("InGameUIManger").GetComponent<InGameUIManger>();
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
        if (cameraManger == null) SetCameraManger();
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
        InGameSetting();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MergeTestSenes")
        {
            print("DDD");
            InGameSetting();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
