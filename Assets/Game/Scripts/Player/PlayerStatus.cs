using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus 
{
    public int hp;
    public int maxHp;
    public float moveSpeed;

    public float reLoadTime;
    public int maxBullet;
    public int getBullet;

    public float attack_tickRate;

    public float attackPower;
    public float reLoadSpeedUp;
    public float rangeUp;
    public float attackRangeUp;
    public bool fCollision;
    public bool isLive;
    
}
