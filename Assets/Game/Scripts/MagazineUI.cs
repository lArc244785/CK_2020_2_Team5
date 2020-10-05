using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineUI : MonoBehaviour
{
    private EnumInfo.MagazineUI state;
    private PlayerControl pc;
    private Text magazineText;
    // Start is called before the first frame update
    void Start()
    {
        magazineText = transform.Find("Magazine_Text").GetComponent<Text>();
        setState(EnumInfo.MagazineUI.Reroad);
        pc = GameManger.instance.getPlayerControl();
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
                DrawNoEmpty(pc.getBullet(), pc.getMaxBullet());
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
