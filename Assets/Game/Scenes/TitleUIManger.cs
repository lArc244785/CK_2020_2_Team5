using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManger : MonoBehaviour
{
    public void GameStart()
    {
        GameManger.instance.GoToInGameScene();
    }

    public void GameExit()
    {
        GameManger.instance.GameExit();
    }
}
