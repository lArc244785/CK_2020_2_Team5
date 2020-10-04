using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingView : MonoBehaviour
{
    private Animator loadAni;
    private GameObject LoadingView;
    private GameObject LoadTextAndImg;
    private Animator LoadAni;

    public void Setting()
    {
        LoadingView = transform.GetChild(0).gameObject;
        loadAni = LoadingView.GetComponent<Animator>();
        LoadTextAndImg = GameObject.Find("LoadTextAndImg");
        LoadAni = GameObject.Find("LoadingImg").GetComponent<Animator>();
        LoadAni.speed = 1.5f;
    }

    public void PadeIn()
    {
        loadAni.SetTrigger("PadeIn");

    }

    public void PadeOut()
    {
        loadAni.SetTrigger("PadeOut");
    }


    public bool GetLoadingView()
    {
        return LoadingView.active;
    }


    public void EventEnd()
    {
        loadAni.SetTrigger("End");
    }

    public void SetLoadingViewActive(bool isVisable) 
    {
        Debug.Log("CODE 7894");
        LoadingView.SetActive(isVisable);
    }

    public void SetLoadTextAndImg(bool isVisable)
    {
        LoadTextAndImg.SetActive(isVisable);
    }

    public void LoadImgAniStart()
    {
        LoadAni.SetTrigger("Loading");
    }

    public void LoadImgAniStop()
    {
        LoadAni.SetTrigger("End");
    }

}
