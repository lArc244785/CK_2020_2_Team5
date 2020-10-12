using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_UI : I_UI
{


    public override void Setting(GameObject obj)
    {
        base.Setting(GameObject.Find("UI_GameOverResourceParent"));
        DrawUIObject.SetActive(false);
    }

    public override void Draw(bool isVisable)
    {
        base.Draw(isVisable);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverPadeIn());
    }

    IEnumerator GameOverPadeIn()
    {
        yield return new WaitForSeconds(2.0f);
        GameManger.instance.SetGameState(EnumInfo.GameState.Loading);
        UI_LoadingView loadView = GameManger.instance.GetLoadingView();
        loadView.SetPadeinOutOption(EnumInfo.PadeinOutOption.GameOver);
        loadView.PadeIn();
        yield return new WaitForSeconds(0.7f);
        Draw(true);
        loadView.PadeOut();
        yield return new WaitForSeconds(1.0f);
        loadView.SetPadeinOutOption(EnumInfo.PadeinOutOption.StageMove);
        loadView.SetLoadingViewActive(false);
        GameManger.instance.SetGameState(EnumInfo.GameState.GameOver);

    }



}
