using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    
    private GameObject player;
    private StageManger stageManger;


     private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            Setting();
            DontDestroyOnLoad(this);
        }
        else
        {
            GameObject.Destroy(this);
        }
    }


    private void Setting()
    {
        SetPlayer();
        SetStageManger();
    }

    public void SetPlayer()
    {
        player =  GameObject.Find("Player");
        Debug.Log("Player " + player.name);
    }

    public void SetStageManger()
    {
        stageManger = GameObject.Find("StageManger").GetComponent<StageManger>();
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public StageManger getStageManger()
    {
        return stageManger;
    }
    

}
