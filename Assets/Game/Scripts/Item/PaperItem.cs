using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperItem : I_Item
{

    //테스트용입니다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Code 500: InPlayer");
            switch (item)
            {
                case EnumInfo.Item.Power:
                case EnumInfo.Item.Range:
                case EnumInfo.Item.Reload:
                case EnumInfo.Item.Speed:
                    GameManger.instance.getInGameUIMangerI().ItemGet(item);
                    break;
                default:
                    Debug.LogWarning("Code: 1000 : 잘 못된 값이 들어왔습니다. " + other);
                    break;
            }
            gameObject.SetActive(false);
        }
    }

    //아이템 획득했을때 이 메소드를 호출해주시면 돕니다.
    public void ItemGet()
    {
        switch (item)
        {
            case EnumInfo.Item.Power:
            case EnumInfo.Item.Range:
            case EnumInfo.Item.Reload:
            case EnumInfo.Item.Speed:
                GameManger.instance.getInGameUIMangerI().ItemGet(item);
                break;
            default:

                break;
        }
        
    }
    
}