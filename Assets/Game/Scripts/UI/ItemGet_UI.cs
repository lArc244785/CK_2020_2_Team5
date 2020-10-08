using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet_UI : I_UI
{
    public Sprite[] items;
    private TextMeshProUGUI explainText;
    private Image itemImg;
    private bool isEventEnd;

    public override void Setting(GameObject obj)
    {
        base.Setting(transform.GetChild(0).gameObject);
        GameObject objText = GameObject.Find("UI_explainText");
        Debug.Log("AA: " + objText);
        explainText = objText.GetComponentInChildren<TextMeshProUGUI>();
        if (explainText == null) Debug.LogError("Code 995: explainText Null");
        itemImg = GameObject.Find("UI_itemImg").GetComponent<Image>();
        DrawUIObject.SetActive(false);
    }



    public override void Draw(bool isviable)
    {
        base.Draw(isviable);
    }
    //아이템 회득 UI활성화 시키는것 입니다.
    public void ItemGet(EnumInfo.Item item)
    {
        UISetting(item);
        GameManger.instance.SetGameState(EnumInfo.GameState.ItemGet);
    }

    public void CloseUI()
    {
        //플레이어의 컨트롤를 잠금해제 넣어야됨
        isEventEnd = true;
        DrawUIObject.SetActive(false);
        GameManger.instance.SetGameState(EnumInfo.GameState.Ingame);
        Time.timeScale = 1.0f;
    }

    private void UISetting(EnumInfo.Item item)
    {
        string context = null;
        switch (item)
        {
            case EnumInfo.Item.Power:
                context = "공격력 강화의 두루마리를 얻었다!";
                itemImg.sprite = items[0];
                break;
            case EnumInfo.Item.Range:
                context = "사거리 증가의 두루마리를 얻었다!";
                itemImg.sprite = items[1];
                break;
            case EnumInfo.Item.Reload:
                context = "재장전 감소의 두루마리를 얻었다!";
                itemImg.sprite = items[2];
                break;
            case EnumInfo.Item.Speed:
                context = "이동속도 증가의 두루마리를 얻었다!";
                itemImg.sprite = items[3];
                break;
        }

        explainText.text = context;
    }



    public bool GetIsStringEventEnd()
    {
        return isEventEnd;
    }


}
