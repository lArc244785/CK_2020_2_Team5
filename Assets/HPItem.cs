using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    public int HP = 1;


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //플레이어 회복메소드를 넣어주세요

            //이거는 UI부분입니다.
            GameManger.instance.getInGameUIManger().GetHpUI().AddHp(HP);
        }
    }

}
