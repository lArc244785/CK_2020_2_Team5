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
        GameManger.instance.SetGameState(EnumInfo.GameState.Loading);
        UI_LoadingView loadView = GameManger.instance.GetLoadingView();

        loadView.PadeIn();
        yield return new WaitForSeconds(0.7f);
        Draw(true);
        loadView.SetSpeed(0.25f);
        loadView.PadeOut();
        yield return new WaitForSeconds(1.0f);
        loadView.SetLoadingViewActive(false);
        loadView.SetSpeed(1.0f);
        GameManger.instance.SetGameState(EnumInfo.GameState.GameOver);

    }



}
