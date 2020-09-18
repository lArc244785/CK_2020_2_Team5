using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanonUI : I_UI
{
    RectTransform canonImgParentRectTr;
    private List<Image> cannonImgList;
    private List<Animator> canonImgAniList;

    private EnumInfo.CanonState state;
    private PlayerControl pc;
    // Start is called before the first frame update
    private int canonNum;
    private int canonMaxNum;

    private float moveX = 80.0f;
    private float minX = 0.0f;
    private float maxX = 400.0f;

    private Vector2 targetPos;
    private Vector2 currentPos;
    private Vector2 beforeCurrentPos;
    private Vector2 currentPosValue;

    private float RemoveSmoothTime= 0.05f;

    private float SmoothTime ;



    public override void Setting()
    {
        setState(EnumInfo.CanonState.BulletOn);
        pc = GameManger.instance.getPlayerControl();
        canonImgParentRectTr = transform.GetChild(0).GetComponent<RectTransform>();
        print("CODE 232: " + canonImgParentRectTr.GetChildCount());
        cannonImgList = new List<Image>();
        canonImgAniList = new List<Animator>();
        for (int i = 0; i < canonImgParentRectTr.GetChildCount(); i++)
        {
            GameObject canon = canonImgParentRectTr.GetChild(i).gameObject;
            cannonImgList.Add(canon.GetComponent<Image>());
            canonImgAniList.Add(canon.GetComponent<Animator>());
        }

        targetPos= canonImgParentRectTr.localPosition;
        currentPos = canonImgParentRectTr.localPosition;

        SmoothTime = RemoveSmoothTime;

        CanonUISet(GameManger.instance.getPlayerControl().getMaxBullet());
    }


    public void setState(EnumInfo.CanonState state)
    {
        this.state = state;
    }

    public void Draw()
    {
        for (int i = 0; i < cannonImgList.Count; i++)
        {
            if (i < canonNum)
            {
                cannonImgList[i].enabled = true;
            }
            else
            {
                cannonImgList[i].enabled = false;
            }
        }

        switch (state)
        {
            case EnumInfo.CanonState.Reroad:
                canonImgParentRectTr.DOLocalMoveX(targetPos.x, SmoothTime);
                break;
        }
      
    }

    private void CanonUISet(int Bullet)
    {
        canonMaxNum = Bullet;
        canonNum = canonMaxNum;
        Draw();
        float parentLoaclX = maxX - moveX * canonNum;
        parentLoaclX = Mathf.Clamp(parentLoaclX, minX, maxX);
        canonImgParentRectTr.localPosition = new Vector3(parentLoaclX, 0, 0);
        targetPos = canonImgParentRectTr.localPosition;
        currentPos = targetPos;
    }


    public void ShootBullet(int bullet)
    {
        canonNum = bullet;
        print("Code 4987: Shoot");
        //CanonTargetPosSet(false);
    }


    public void ReLoadEvent(float ReloadTime)
    {
        Draw();
        canonImgParentRectTr.localPosition = new Vector3(maxX, 0, 0);
        targetPos.x = maxX;
        state = EnumInfo.CanonState.Reroad;
        StartCoroutine(ReLoadCorutine(ReloadTime));
    }

    IEnumerator ReLoadCorutine(float ReloadTime)
    {
        
        float reloadTic = ReloadTime / 6.0f;
        SmoothTime = reloadTic - 0.05f;
        yield return new WaitForSeconds(reloadTic);
        for (int i = 0; i < canonMaxNum; i++)
        {
            canonNum++;
            canonImgAniList[i].SetTrigger("Reload");
            CanonTargetPosSet(true);
            yield return new WaitForSeconds(reloadTic);
        }
        state = EnumInfo.CanonState.BulletOn;
        yield return 0;
    }

    private void CanonTargetPosSet(bool isAdd)
    {
        float move = isAdd ? -moveX : moveX;

        Vector2 pos = new Vector2(targetPos.x + move, 0);
        print("CODE 7997: " + pos);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        targetPos = pos;
        
    }

}
