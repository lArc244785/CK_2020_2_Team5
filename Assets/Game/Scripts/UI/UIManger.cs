using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger :I_UI
{
    private CanonUI canonUI;
    private HPUI hpUI;

    // Start is called before the first frame update




    public override void Setting()
    {
        canonUI = GameObject.Find("UI_Canon").GetComponent<CanonUI>();
        hpUI = GameObject.Find("UI_HP").GetComponent<HPUI>();

        canonUI.Setting();
        hpUI.Setting();
    }

    public override void Draw()
    {
        switch (GameManger.instance.GetGameState())
        {
            case EnumInfo.GameState.Ingame:
                hpUI.Draw();
                canonUI.Draw();
                break;
        }
    }



    public void Update()
    {
        Draw();
    }

    public CanonUI getcanonUI()
    {
        return canonUI;
    }

}
