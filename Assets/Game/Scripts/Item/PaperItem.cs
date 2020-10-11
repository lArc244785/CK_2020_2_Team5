using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperItem : I_Item
{
    public GameObject player;

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
                    GameManger.instance.getInGameUIManger().ItemGet(item);
                    break;
                default:
                    Debug.LogWarning("Code: 1000 : 잘 못된 값이 들어왔습니다. " + other);
                    break;
            }
            ItemGet();
            gameObject.SetActive(false);
        }
    }

    //아이템 획득했을때 이 메소드를 호출해주시면 돕니다.
    public void ItemGet()
    {
        switch (item)
        {
            case EnumInfo.Item.Power:
                player.GetComponent<PlayerControl>().StatUp_PowerUp();
                break;
            case EnumInfo.Item.Range:
                player.GetComponent<PlayerControl>().StatUp_AttackRange();
                break;
            case EnumInfo.Item.Reload:
                player.GetComponent<PlayerControl>().StatUp_ReLoadUp();
                break;
            case EnumInfo.Item.Speed:
                player.GetComponent<PlayerControl>().StatUp_SpeedUp();
                break;
                
            default:

                break;
        }
        GameManger.instance.getInGameUIManger().ItemGet(item);
    }
    
}