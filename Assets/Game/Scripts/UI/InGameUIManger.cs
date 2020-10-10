using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManger :I_UI
{
    private CanonUI canonUI;
    private HPUI hpUI;
    private ItemGet_UI itemUI;
    private GameClearUI gameClearUI;
    private Pause_UI pauseUI;
    private GameOver_UI gameOverUI;

    // Start is called before the first frame update

    public bool TestMode;


    public override void Setting(GameObject obj)
    {
        print("InGameSetting");

        canonUI = GameObject.Find("UI_Canon").GetComponent<CanonUI>();
        hpUI = GameObject.Find("UI_HP").GetComponent<HPUI>();
        itemUI = GameObject.Find("UI_ItemGet").GetComponent<ItemGet_UI>();
        gameClearUI = GameObject.Find("UI_GameClear").GetComponent<GameClearUI>();
        pauseUI = GameObject.Find("UI_Pause").GetComponent<Pause_UI>();
        gameOverUI = GameObject.Find("UI_GameOver").GetComponent<GameOver_UI>();

 
        canonUI.Setting(null);
        hpUI.Setting(null);
        itemUI.Setting(null);
        gameClearUI.Setting(null);
        pauseUI.Setting(null);
        gameOverUI.Setting(null);
    }

    public override void Draw(bool isVisable)
    {
        if(GameManger.instance.GetGameState() == EnumInfo.GameState.Ingame)
        {
            gameOverUI.Draw(false);
            itemUI.Draw(false);
            gameClearUI.Draw(false);
            pauseUI.Draw(false);

            hpUI.Draw(true);
            canonUI.Draw(true);
        }
        else
        {
            hpUI.Draw(false);
            canonUI.Draw(false);
            switch (GameManger.instance.GetGameState())
            {
                case EnumInfo.GameState.Pause:
                    pauseUI.Draw(true);
                    break;
                case EnumInfo.GameState.GameOver:
                    gameOverUI.Draw(true);
                    break;
                case EnumInfo.GameState.ItemGet:
                    itemUI.Draw(true);
                    break;
                case EnumInfo.GameState.GameClear:
                    gameClearUI.Draw(true);
                    break;
            }
        }

    }

    private void DrawVisable()
    {
        hpUI.Draw(false);
        pauseUI.Draw(false);
        gameOverUI.Draw(false);
        itemUI.Draw(false);
        gameClearUI.Draw(false);
    }


    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUIOppenAndClose();
        }

        if (TestMode)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                itemUI.ItemGet(EnumInfo.Item.Power);
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                gameClearUI.GameClear();
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                gameOverUI.GameOver();
            }
        }

        Draw(true);
    }

    public CanonUI getcanonUI()
    {
        return canonUI;
    }

    //현재의 게임 상태에 따라 pause창이 열릴지 말지를 결정한다.
    public void PauseUIOppenAndClose()
    {
        EnumInfo.GameState gameState = GameManger.instance.GetGameState();
        if (gameState == EnumInfo.GameState.Ingame) GameManger.instance.SetGameState(EnumInfo.GameState.Pause);
        else if (gameState == EnumInfo.GameState.Pause) GameManger.instance.SetGameState(EnumInfo.GameState.Ingame);
        else return;
    }

    public void GoToTitle()
    {
        GameManger.instance.GoToTitleScene();
    }
    public void GoToInGame()
    {
        GameManger.instance.GoToInGameScene();
    }

    public void ItemGet(EnumInfo.Item item)
    {
        GameManger.instance.SetGameState(EnumInfo.GameState.ItemGet);
        itemUI.ItemGet(item);
    }

    public GameOver_UI GetGameOverUI()
    {
        return gameOverUI;
    }

    public GameClearUI GetGameClearUI()
    {
        return gameClearUI;
    }

    public HPUI GetHpUI()
    {
        return hpUI;
    }
}
