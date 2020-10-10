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

    private float moveX = 70.0f;
    private float minX = 54.0f;
    private float maxX = 390;

    private float posY;

    private Vector2 targetPos;

    private float RemoveSmoothTime= 0.05f;

    private float SmoothTime ;

    private Image backGround;

    public override void Setting(GameObject obj)
    {
        base.Setting(transform.GetChild(0).gameObject);
        setState(EnumInfo.CanonState.BulletOn);
        pc = GameManger.instance.getPlayerControl();
        canonImgParentRectTr = DrawUIObject.transform.GetChild(0).GetComponent<RectTransform>();
        print("CODE 232: " + canonImgParentRectTr.childCount);
        cannonImgList = new List<Image>();
        canonImgAniList = new List<Animator>();
        for (int i = 0; i < canonImgParentRectTr.childCount; i++)
        {
            GameObject canon = canonImgParentRectTr.GetChild(i).gameObject;
            cannonImgList.Add(canon.GetComponent<Image>());
            canonImgAniList.Add(canon.GetComponent<Animator>());
        }

        targetPos= canonImgParentRectTr.localPosition;

        SmoothTime = RemoveSmoothTime;

        posY = canonImgParentRectTr.localPosition.y;
        backGround = DrawUIObject.GetComponent<Image>();

        CanonUISet(GameManger.instance.getPlayerControl().getMaxBullet());
    }


    public void setState(EnumInfo.CanonState state)
    {
        this.state = state;
    }

    public override void Draw(bool isVisable)
    {
        base.Draw(isVisable);

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
                backGround.color = Color.red;
                canonImgParentRectTr.DOLocalMoveX(targetPos.x, SmoothTime);
                break;
            case EnumInfo.CanonState.BulletOn:
                backGround.color = Color.white;
                break;
        }
      
    }

    private void CanonUISet(int Bullet)
    {
        canonMaxNum = Bullet;
        canonNum = canonMaxNum;
        Draw(DrawUIObject.activeSelf);
        float parentLoaclX = maxX - moveX * canonNum;
        parentLoaclX = Mathf.Clamp(parentLoaclX, minX, maxX);
        canonImgParentRectTr.localPosition = new Vector3(parentLoaclX, posY, 0);
        targetPos = canonImgParentRectTr.localPosition;
    }


    public void ShootBullet(int bullet)
    {
        canonNum = bullet;
        print("Code 4987: Shoot");
        //CanonTargetPosSet(false);
    }


    public void ReLoadEvent(float ReloadTime)
    {
        Draw(DrawUIObject.activeSelf);
        canonImgParentRectTr.localPosition = new Vector3(maxX, posY, 0);
        targetPos.x = maxX;
        state = EnumInfo.CanonState.Reroad;
        StartCoroutine(ReLoadCorutine(ReloadTime));
    }

    IEnumerator ReLoadCorutine(float ReloadTime)
    {
    
        float reloadTic = ReloadTime / 5.0f;
        float animationSpeed = 1.0f / reloadTic; // 애니메이션이 Tic의 시간안에 모두 돌아가야되서 얼마나 배속을 해야되는지 연산
        SmoothTime = reloadTic - 0.05f;
        yield return new WaitForSeconds(reloadTic);
        for (int i = 0; i < canonMaxNum; i++)
        {
            canonNum++;
            canonImgAniList[i].speed = animationSpeed;
            canonImgAniList[i].SetTrigger("Reload");
            CanonTargetPosSet(true);
            yield return new WaitForSeconds(reloadTic);
            canonImgAniList[i].speed = 1.0f;
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
