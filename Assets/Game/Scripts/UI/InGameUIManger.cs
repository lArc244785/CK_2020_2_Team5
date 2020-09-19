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


    public override void Setting()
    {
        canonUI = GameObject.Find("UI_Canon").GetComponent<CanonUI>();
        hpUI = GameObject.Find("UI_HP").GetComponent<HPUI>();
        itemUI = GameObject.Find("UI_ItemGet").GetComponent<ItemGet_UI>();
        gameClearUI = GameObject.Find("UI_GameClear").GetComponent<GameClearUI>();
        pauseUI = GameObject.Find("UI_Pause").GetComponent<Pause_UI>();
        gameOverUI = GameObject.Find("UI_GameOver").GetComponent<GameOver_UI>();


        canonUI.Setting();
        hpUI.Setting();
        itemUI.Setting();
        gameClearUI.Setting();
        pauseUI.Setting();
        gameOverUI.Setting();
    }

    public override void Draw()
    {
        switch (GameManger.instance.GetGameState())
        {
            case EnumInfo.GameState.Ingame:
                hpUI.Draw();
                canonUI.Draw();
                break;
            case EnumInfo.GameState.Pause:
                pauseUI.Draw();
                break;
            case EnumInfo.GameState.GameOver:
                gameOverUI.Draw();
                break;
        }
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUIOppenAndClose();
        }

        Draw();
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

        gameState = GameManger.instance.GetGameState();
        bool isPauseDraw = gameState == EnumInfo.GameState.Ingame ? false : true;

        pauseUI.setDraw(isPauseDraw);
    }

    public void GoToTitle()
    {
        GameManger.instance.GoToTitleScene();
    }
    public void GoToInGame()
    {
        GameManger.instance.GoToInGameScene();
    }
}
