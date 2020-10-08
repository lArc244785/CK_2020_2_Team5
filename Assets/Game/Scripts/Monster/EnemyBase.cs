using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyBase : MonoBehaviour
{
    public MonsterStatus mstatus;

    /// <summary>
    /// 공격
    /// </summary>
    public virtual void Attack()
    {

    }

    /// <summary>
    /// 플레이어 추적
    /// </summary>
    public virtual void TracePlayer()
    {

    }

    /// <summary>
    /// 대기
    /// </summary>
    public virtual void Wait()
    {

    }

    /// <summary>
    /// 범위내 플레이어 확인용
    /// </summary>
    public virtual void FindPlayer()
    {

    }

    /// <summary>
    /// 배회용 거리,방향계산
    /// </summary>
    public virtual void RandomMove()
    {

    }

    /// <summary>
    /// 공격당함
    /// </summary>
    public virtual void OnDamage()
    {

    }

    /// <summary>
    /// 움직임
    /// </summary>
    public virtual void Move()
    {

    }

    /// <summary>
    /// 앞에 장애물이 있는가
    /// </summary>
    /// <param name="isco"></param>
    public void IsCollision(bool isco)
    {
        mstatus.fcollision = isco;
    }

}
