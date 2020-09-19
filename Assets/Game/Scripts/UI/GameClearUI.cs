using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : I_UI
{
    private GameObject gameClearUI;

    public override void Setting()
    {
        gameClearUI = GameObject.Find("UI_ClearParent").gameObject;
        gameClearUI.SetActive(false);
    }

    //이 메소드를 불러주면 클리어화면이 나옵니다.
    public override void Draw()
    {
        gameClearUI.SetActive(true);
    }


}
