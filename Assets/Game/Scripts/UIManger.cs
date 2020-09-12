using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    private MagazineUI magazineUI;

    // Start is called before the first frame update

    private void Start()
    {
        Setting();
    }


    public void Setting()
    {
        magazineUI = GameObject.Find("Magazine_UI").GetComponent<MagazineUI>();
    }

    public MagazineUI getMagazineUI()
    {
        return magazineUI;
    }

    public void Update()
    {
        switch (GameManger.instance.GetGameState())
        {
            case EnumInfo.GameState.Ingame:
                magazineUI.Draw();
                break;
        }
    }

}
