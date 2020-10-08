using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause_UI : I_UI
{
    public Image[] selectionImgs;

    public override void Setting(GameObject obj)
    {
        base.Setting(GameObject.Find("UI_PauseResourceParent"));
        DrawUIObject.SetActive(false);
    }
    public override void Draw(bool isvisable)
    {
        base.Draw(isvisable);
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







}
