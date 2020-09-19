using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet_UI : I_UI
{
    public Sprite[] items;
    private GameObject ItemGetParent;
    private Text explainText;
    private Image itemImg;

    public override void Setting()
    {
        ItemGetParent = transform.GetChild(0).gameObject;
        GameObject obj = GameObject.Find("UI_explainText");
        Debug.Log("AA: " + obj);
        explainText = obj.GetComponentInChildren<Text>();
        if (explainText == null) Debug.LogError("Code 995: explainText Null");
        itemImg = GameObject.Find("UI_itemImg").GetComponent<Image>();
        ItemGetParent.SetActive(false);
    }



    public override void Draw()
    {
        ItemGetParent.SetActive(true);

    }
    //아이템 회득 UI활성화 시키는것 입니다.
    public void ItemGet(EnumInfo.ItemGet item)
    {
        //플레이어의 컨트롤를 잠그는거 넣어야됨
        Time.timeScale = 0.0f;
        UISetting(item);
        Draw();
    }

    public void CloseUI()
    {
        //플레이어의 컨트롤를 잠금해제 넣어야됨
        ItemGetParent.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void UISetting(EnumInfo.ItemGet item)
    {
        string context = null;
        switch (item)
        {
            case EnumInfo.ItemGet.Power:
                context = "공격력 강화의 두루마리를 얻었다!";
                itemImg.sprite = items[0];
                break;
            case EnumInfo.ItemGet.Range:
                context = "사거리 증가의 두루마리를 얻었다!";
                itemImg.sprite = items[1];
                break;
            case EnumInfo.ItemGet.Reload:
                context = "재장전 감소의 두루마리를 얻었다!";
                itemImg.sprite = items[2];
                break;
            case EnumInfo.ItemGet.Speed:
                context = "이동속도 증가의 두루마리를 얻었다!";
                itemImg.sprite = items[3];
                break;
        }
        
        explainText.text = context;
    }

}
