using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : I_UI
{
    private List<Image> imageList;
    public int HP;

    public Sprite blankHpSprite;
    public Sprite fullHpSprite;

    public override void Setting()
    {
        HpImagesSetting();
    }
    public void HpImagesSetting()
    {
        Transform tr = transform;
        imageList = new List<Image>();
        for (int i = 0; i < tr.GetChildCount(); i++)
        {
            imageList.Add(tr.GetChild(i).GetComponent<Image>());
        }
    }

    public override void Draw()
    {
        float uiHpValue = HP;


        for (int i = 0; i < imageList.Count; i++)
        {
            if (i < uiHpValue)
            {
                imageList[i].sprite = fullHpSprite;
            }
            else
            {
                imageList[i].sprite = blankHpSprite;
            }
        }
    }

    public void SetHp(int hp)
    {
        HP = hp;
        Draw(); 
    }

    public void AddHp(int hp)
    {
        HP += hp;
        Draw();
    }

    public void DecreaseHp(int hp)
    {
        HP -= hp;
        Draw();
    }
}
