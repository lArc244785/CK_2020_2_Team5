using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    private Room room;
    private List<GameObject> monsterList = new List<GameObject>();
    private bool isMonstersALive = true;
    private void Start()
    {
        room = transform.GetComponentInParent<Room>();
        monsterListSetting();
    }

    public void monsterListSetting()
    {
        Transform tr = transform;

        monsterList.Clear();
        for (int i = 0; i < tr.GetChildCount(); i++)
        {
            try
            {
                GameObject temp = tr.GetChild(i).gameObject;
                Debug.Log("Monster GameObject  " + temp.gameObject);
                monsterList.Add(temp);
            }
            catch
            {
                Debug.LogError("MonsterGenerator_monsterListSettingNullObject Index: " + i);
            }
        }
    }


    public bool Spawn()
    {
        return true;
    }

    private void Update()
    {
        if (room.getIsPlayer() && isMonstersALive)
        {
            Debug.Log("Monster  ALive");
            isMonstersALive = ChackMonsterDead();
            if(isMonstersALive == false)
            {
                room.RoomClear();
            }
        }
    }

    public bool ChackMonsterDead()
    {
        foreach(GameObject obj in monsterList)
        {
            if (obj.activeSelf == true) return true;
        }
        return false;
    }
}
