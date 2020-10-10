using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    //테스트용
    //public void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        GameManger.instance.getInGameUIManger().GetGameClearUI().GameClear();
    //    }
    //}

    //위 메소드를 사용하시면 됩니다.
    public void GameClearEvent()
    {
        GameManger.instance.getInGameUIManger().GetGameClearUI().GameClear();
    }
}
