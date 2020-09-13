using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    private MagazineUI magazineUI;
    private DashUI dashUI;
    // Start is called before the first frame update




    public void Setting()
    {
        magazineUI = GameObject.Find("Magazine_UI").GetComponent<MagazineUI>();
        dashUI = GameObject.Find("Dash_UI").GetComponent<DashUI>();
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
                
                EnumInfo.MagazineUI mu = GameManger.instance.getPlayerControl().getIsReload() ? EnumInfo.MagazineUI.Reroad : EnumInfo.MagazineUI.NoEmpty;
                magazineUI.setState(mu);
                magazineUI.Draw();

                dashUI.Draw();

                break;
        }
    }

}
