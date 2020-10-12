using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    private Stage room;
    private List<GameObject> monsterList = new List<GameObject>();
    private bool isMonstersALive = true;


    private void Awake()
    {
        room = transform.GetComponentInParent<Stage>();
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
                //Debug.Log("Monster GameObject  " + temp.gameObject);
                monsterList.Add(temp);
               
            }
            catch
            {
                Debug.LogError(gameObject);
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
                if(monsterList[i].GetComponent<LongEnemy>() != null)
                {
                    monsterList[i].GetComponent<LongEnemy>().enabled = false;
                }
                else if(monsterList[i].GetComponent<ShortEnemy>() != null)
                {
                    monsterList[i].GetComponent<ShortEnemy>().enabled = false;
                }
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
                if (monsterList[i].GetComponent<LongEnemy>() != null)
                {
                    monsterList[i].GetComponent<LongEnemy>().enabled = true;
                }
                else if (monsterList[i].GetComponent<ShortEnemy>() != null)
                {
                    monsterList[i].GetComponent<ShortEnemy>().enabled = true;
                }
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
            else
            {
                Debug.Log("CODE 443: "+ obj.name);
            }
        }
        return false;
    }
}
