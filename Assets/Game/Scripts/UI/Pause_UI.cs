using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause_UI : I_UI
{
    public Image[] selectionImgs;
    private GameObject PauseResourceParent;

    public override void Setting()
    {
        base.Setting();
        PauseResourceParent = GameObject.Find("UI_PauseResourceParent");
        setDraw(false);
    }
    public override void Draw()
    {

        for (int i = 0; i < selectionImgs.Length; i++)
        {
            selectionImgs[i].enabled = false;
        }

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            print("code 3322: " + raycastResults[i].gameObject + "   " + raycastResults[i].gameObject.tag);
            if (raycastResults[i].gameObject.tag == "PauseUI_Button")
            {
                raycastResults[i].gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
                break;
            }
        }

    }


    public void setDraw(bool isDraw)
    {

        switch (GameManger.instance.GetGameState())
        {
            case EnumInfo.GameState.Ingame:
            case EnumInfo.GameState.Pause:
                PauseResourceParent.SetActive(isDraw);
                break;
            default:
                Debug.LogError("CODE 3002: " + GameManger.instance.GetGameState());
                PauseResourceParent.SetActive(false);
                break;
        }

    }




}
