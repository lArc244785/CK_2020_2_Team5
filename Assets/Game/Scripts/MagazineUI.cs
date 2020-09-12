using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineUI : MonoBehaviour
{
    private EnumInfo.MagazineUI state;

    private Text magazineText;
    // Start is called before the first frame update
    void Start()
    {
        magazineText = transform.FindChild("Magazine_Text").GetComponent<Text>();
        setState(EnumInfo.MagazineUI.Reroad);
    }


    public void setState(EnumInfo.MagazineUI state)
    {
        this.state = state;
    }

    public void Draw()
    {
        switch (state)
        {
            case EnumInfo.MagazineUI.NoEmpty:
                //플레이어 정보 받아오기
                break;
            case EnumInfo.MagazineUI.Reroad:
                DrawReroad();
                break;
        }
    }

    private void DrawNoEmpty(int magazine, int maxMagazine)
    {
        magazineText.text = magazine + "/" + maxMagazine;
    }

    private void DrawReroad()
    {
        magazineText.text = "Reroading...";
    }

}
