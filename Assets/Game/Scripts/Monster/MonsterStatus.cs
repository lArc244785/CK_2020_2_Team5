﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStatus 
{
    public int hp;
    public int maxHp;

    public float shortMonsterRange; //플레이어로 이동하기위한 거리
    public float longMonsterRange;

    public float tick;
    public float tickRate;

    public float shortdamage; //각각 단거리-장거리 몬스터의 데미지
    public float longdamage;

    public float bulletSpeed;

    public float shortAttackRange; //공격 사거리
    public float longAttackRange;

    public bool isShortMonster =false; //근거리 몬스터인지 장거리 몬스터인지
    public bool isLongMonster =false;

    public void Initialize()
    {
        hp = maxHp;
        tick = 0;
    }
}
