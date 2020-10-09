using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : I_UI
{

    public override void Setting(GameObject obj)
    {
        base.Setting(GameObject.Find("UI_ClearParent"));
        DrawUIObject.SetActive(false);
    }

    //이 메소드를 불러주면 클리어화면이 나옵니다.
    public override void Draw(bool isVisable)
    {
        base.Draw(isVisable);
    }

    public void GameClear()
    {
        StartCoroutine(GameClearPadeInOutEvent());
    }

    IEnumerator GameClearPadeInOutEvent()
    {
        GameManger.instance.SetGameState(EnumInfo.GameState.Loading);
        UI_LoadingView loadView = GameManger.instance.GetLoadingView();
        loadView.SetPadeinOutOption(EnumInfo.PadeinOutOption.GameClear);
        loadView.PadeIn();
        yield return new WaitForSeconds(0.7f);
        Draw(true);
        loadView.PadeOut();
        yield return new WaitForSeconds(1.0f);
        loadView.SetPadeinOutOption(EnumInfo.PadeinOutOption.StageMove);
        loadView.SetLoadingViewActive(false);
        GameManger.instance.SetGameState(EnumInfo.GameState.GameClear);
    }


}
