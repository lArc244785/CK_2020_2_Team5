using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManger : MonoBehaviour
{
    private List<Stage> stageList = new List<Stage>();



    public void Setting()
    {
        Transform tr = transform;
        for (int i = 0; i < tr.childCount; i++)
        {
            try
            {
                Stage tempRoom = tr.GetChild(i).GetComponent<Stage>();
                stageList.Add(tempRoom);
            }
            catch 
            {
                Debug.LogError("StageManger_RoomSettting Error: " + i);
                return;
            }
         
        }
    }

    public Stage getStage(int index)
    {
        if (index > stageList.Count) return null;
        return stageList[index];
    }

}
