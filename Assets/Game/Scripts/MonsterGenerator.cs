using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    private Room room;
    private List<GameObject> monsterList = new List<GameObject>();
    private bool isMonstersALive = true;
    private void Awake()
    {
        room = transform.GetComponentInParent<Room>();
        monsterListSetting();
        MonsterEventOff();
    }

    public void monsterListSetting()
    {
        Transform tr = transform;

        monsterList.Clear();
        for (int i = 0; i < tr.childCount; i++)
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

    public void MonsterEventOff()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            try
            {
                MonsterControl mc = monsterList[i].GetComponent<MonsterControl>();
                mc.enabled = false;
            }
            catch
            {
                Debug.LogError("MonsterEventOffError index: " + i);
                return;
            }
        }
    }

    public void MonsterEventOn()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            try
            {
                MonsterControl mc = monsterList[i].GetComponent<MonsterControl>();
                mc.enabled = true;
            }
            catch
            {
                Debug.LogError("MonsterEventOffError index: " + i);
                return;
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
            //Debug.Log("Monster  ALive");
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
