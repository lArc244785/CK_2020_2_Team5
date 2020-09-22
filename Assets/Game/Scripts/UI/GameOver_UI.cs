using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_UI : I_UI
{
    private GameObject gameOverReSourceParent;

    public override void Setting()
    {
        gameOverReSourceParent = GameObject.Find("UI_GameOverResourceParent");
        gameOverReSourceParent.SetActive(false);
    }

    public override void Draw()
    {
        gameOverReSourceParent.SetActive(true);
    }



}
