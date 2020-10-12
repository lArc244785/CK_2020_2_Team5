using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class I_Item : MonoBehaviour
{
    public EnumInfo.Item item;

    public virtual void Event() { }

}
//Stage A
//2.5   4
//2.3   6.7

//Stage B
//4     6.2
//13.7  7.8

//Stage C
//7     4
//0.5   6.8

//Stage D 
//7.25      8.5
//-0.5      1.25

//Stage E
//7.7   8.5
//-2.64 1.25


//0 0.7
//13.5  1.3
