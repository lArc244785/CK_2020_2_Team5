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
    private UI_LoadingView LoadingView;

  

     private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            LoadingView = GameObject.Find("LodingView").GetComponent<UI_LoadingView>();
            LoadingView.Setting();
            LoadingView.SetLoadingViewActive(false);
            LoadingView.SetLoadTextAndImg(false);
        }
        else
        {
            gameObject.SetActive(false);
            GameObject.Destroy(gameObject);
        }
    }



    private void Start()
    {

        if (gameState == EnumInfo.GameState.Ingame)
        {
            print("DDS");
            InGameSetting();

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
        getStageManger().getStage(0).PlayerStageIn(Vector3.zero, Vector3.zero, true);
    }




    public void SetPlayer()
    {
        player =  GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogWarning("No Serch Player " );
            player = GameObject.FindGameObjectWithTag("Player");
            if(player == null)
            {
                Debug.LogWarning("No Serch Tag Player");
            }
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
        inGameManger.Setting(null);
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

    public InGameUIManger getInGameUIManger()
    {
        if (inGameManger == null) SetInGameUIManger();
        return inGameManger;
    }

    public void SetGameState(EnumInfo.GameState state)
    {
        gameState = state;
       // LoadingView.SetLoadingViewActive(false);
        switch (gameState)
        {
            case EnumInfo.GameState.Ingame:
                Time.timeScale = 1;
                break;
            case EnumInfo.GameState.Pause:
                Time.timeScale = 0;
                break;
            case EnumInfo.GameState.Loading:
                LoadingView.SetLoadingViewActive(true);
                break;
        }

    }

    public EnumInfo.GameState GetGameState()
    {
        return gameState;
    }


    public void GoToTitleScene()
    {

        StartCoroutine(IE_LoadingEvnetStart(0));

       // SetGameState(EnumInfo.GameState.Title);
    }


    public void GoToInGameScene()
    {
        StartCoroutine(IE_LoadingEvnetStart(1));
        // SetGameState(EnumInfo.GameState.Ingame);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public UI_LoadingView GetLoadingView()
    {
        return LoadingView;
    }



    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(LoadingView.GetLoadingView())
        StartCoroutine(IE_LoadingEventEnd(scene));
    }

    private IEnumerator IE_LoadingEvnetStart(int i)
    {

        SetGameState(EnumInfo.GameState.Loading);
        Time.timeScale = 1;
        LoadingView.PadeIn();
        yield return new WaitForSeconds(1.0f);
        LoadingView.SetLoadTextAndImg(true);
        LoadingView.LoadImgAniStart();

        if (i == 0)
        {
            SetGameState(EnumInfo.GameState.Title);
        }
        else
        {
            SetGameState(EnumInfo.GameState.Ingame);
        }
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(i);
    }


    private IEnumerator IE_LoadingEventEnd(Scene scene)
    {

        Debug.Log("씬 교체됨, 현재 씬: " + scene.name);

        if (scene.name == "SkulBombs")
        {
            // 씬 전환 효과 (Fade In)
            InGameSetting();

        }
        else
        {
            SetGameState(EnumInfo.GameState.Title);
           
        }
        yield return new WaitForSeconds(1.0f);
        LoadingView.LoadImgAniStop();
        LoadingView.SetLoadTextAndImg(false);
        LoadingView.PadeOut();
        yield return new WaitForSeconds(0.5f);
        LoadingView.SetLoadingViewActive(false);

        if(gameState == EnumInfo.GameState.Ingame)
        {
            LoadingView.SetPadeinOutOption(EnumInfo.PadeinOutOption.StageMove);
            LoadingView.SetLoadingViewActive(false);
        }
        else if(gameState == EnumInfo.GameState.Title)
        {
            LoadingView.LoadOptionReset();
            LoadingView.SetLoadingViewActive(false);
        }

    }




    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
