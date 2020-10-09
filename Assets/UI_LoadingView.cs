using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingView : MonoBehaviour
{
    private Animator CurrentPadeInOutAni;
    private List<GameObject> LoadingViewPadeinoutList;
    private GameObject CurrentPadeinout;
    private GameObject LoadingView_LoadingAni;
    private Animator LoadImgAni;

    private EnumInfo.PadeinOutOption currentPadinoutOption;
    public EnumInfo.PadeinOutOption TitleToInGameOption;


    public void Setting()
    {
        Transform ParentPadeinout = GameObject.Find("LoadingView_LoadPadeinOutList").transform;
        LoadingViewPadeinoutList = new List<GameObject>();
        print(ParentPadeinout.GetChildCount());
        for (int i = 0; i < ParentPadeinout.GetChildCount(); i++)
        {
            LoadingViewPadeinoutList.Add(ParentPadeinout.GetChild(i).gameObject);
            LoadingViewPadeinoutList[i].SetActive(false);
        }

        SetPadeinOutOption(TitleToInGameOption);


        LoadingView_LoadingAni = GameObject.Find("LoadingView_LoadingAni");
        LoadImgAni = GameObject.Find("LoadingView_LoadImg").GetComponent<Animator>();
        LoadImgAni.speed = 1.5f;
    }

    private void PadeinOutExit()
    {
        
        EventEnd();
        SetLoadingViewActive(false);
    }

    public void SetPadeinOutOption(EnumInfo.PadeinOutOption poo)
    {
        int index = (int)poo;
        if(index >= LoadingViewPadeinoutList.Count )
        {
            Debug.LogWarning("Code 4000: LVP_Count: " + LoadingViewPadeinoutList.Count + "poo count: " + (int)poo);
            return;
        }

        PadeinOutExit();

        CurrentPadeinout = LoadingViewPadeinoutList[index];
        CurrentPadeInOutAni = CurrentPadeinout.GetComponent<Animator>();
        SetLoadingViewActive(true);
    }

    public void PadeIn()
    {
        CurrentPadeInOutAni.SetTrigger("PadeIn");

    }

    public void PadeOut()
    {
        if (CurrentPadeInOutAni == null) return;
        CurrentPadeInOutAni.SetTrigger("PadeOut");
    }


    public bool GetLoadingView()
    {
        return CurrentPadeinout.active;
    }


    public void EventEnd()
    {
        if (CurrentPadeInOutAni == null) return;

        CurrentPadeInOutAni.SetTrigger("End");
    }

    public void SetLoadingViewActive(bool isVisable) 
    {
        if (CurrentPadeinout == null) return;
        CurrentPadeinout.SetActive(isVisable);
    }

    public void SetLoadTextAndImg(bool isVisable)
    {
        if (LoadingView_LoadingAni == null) return;
        LoadingView_LoadingAni.SetActive(isVisable);
    }

    public void LoadImgAniStart()
    {
        if (LoadImgAni == null) return;
        LoadImgAni.SetTrigger("Loading");
    }

    public void LoadImgAniStop()
    {
        if (LoadImgAni == null) return;
        LoadImgAni.SetTrigger("End");
    }

    public void LoadOptionReset()
    {
        SetPadeinOutOption(TitleToInGameOption);
        SetLoadingViewActive(false);
    }

    public void SetSpeed(float speed)
    {
        CurrentPadeInOutAni.speed = speed;
    }


}
