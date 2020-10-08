﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LongEnemy : EnemyBase
{
    public NavMeshAgent agent;
    public EnumInfo.MonsterState menum;
    GameObject player;
    public Transform firePos;
    public GameObject bullet;
    public GameObject collisionFoward;
    Quaternion to;
    Vector3 monsvec;

    float xrange = 0f;

    float wait = 0f;

    Vector3 enemyVector;

    public bool isMoving;

    //임시값들
    int rotationnum;


    void Start()
    {
        menum = EnumInfo.MonsterState.RandomMove;
        player = GameObject.FindGameObjectWithTag("Player");
        isMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        switch (menum)
        {
            case EnumInfo.MonsterState.Move:
                break;
            case EnumInfo.MonsterState.Attack:
                
                break;
            case EnumInfo.MonsterState.Trace:
                //TracePlayer();
                break;
            case EnumInfo.MonsterState.Wait:
                Wait();
                break;
            case EnumInfo.MonsterState.Die:
                break;
            case EnumInfo.MonsterState.RandomMove:
                RandomMove();
                break;
            default:
                break;
        }
        Attack();
        FindPlayer();
        IsCollision();
        
    }

    private void FixedUpdate()
    {

        MonsterTurn();
        switch (menum)
        {
            case EnumInfo.MonsterState.Move:
                if (mstatus.isFindPlayer == false)
                {
                    Move();
                }
                else
                    TracePlayer();
                break;
            case EnumInfo.MonsterState.Turn:
                TurnEnemy();
                break;
        }
    }

    //수정필요
    public override void Attack()
    {
        base.Attack();
        mstatus.tick += Time.deltaTime;
        if (mstatus.tick >= mstatus.tickRate && mstatus.isFindPlayer==true)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.longAttackRange)
            {
                Debug.Log("long거리 공격");
                Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
                menum = EnumInfo.MonsterState.Trace;
                mstatus.tick = 0;
            }
            else
            {
                //menum = EnumInfo.MonsterState.Trace;
                //Debug.Log("사거리 부족");
            }
        }

    }

    public override void FindPlayer()
    {
        base.FindPlayer();

        if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.longMonsterRange)// || menum !=EnumInfo.MonsterState.Attack)
        {
            menum = EnumInfo.MonsterState.Move;
            mstatus.isFindPlayer = true;

        }
        else
        {
            mstatus.isFindPlayer = false;
            agent.Stop();
        }
    }

    void MonsterTurn()
    {
        if (mstatus.isFindPlayer==true && Vector3.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
        {
            monsvec = player.transform.position - transform.position;
            transform.forward = monsvec.normalized;

        }
    }

    public override void Wait()
    {
        base.Wait();

        wait += Time.deltaTime;

        if (wait >= mstatus.waitingTime)
        {
            Debug.Log("기다림 끝");
            menum = EnumInfo.MonsterState.RandomMove;
            wait = 0;
        }

    }

    public override void RandomMove()
    {
        base.RandomMove();

        if (isMoving == true)
            return;
        if (mstatus.isFindPlayer == true)
            return;
        Debug.Log("랜덤생성");
        rotationnum = Random.Range(0, 361);
        enemyVector = transform.position;


        xrange = Random.Range(mstatus.minMoveRange, mstatus.maxMoveRange);
        menum = EnumInfo.MonsterState.Turn;

    }


    void TurnEnemy()
    {

        to.eulerAngles = new Vector3(0, rotationnum, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, to, Time.deltaTime*8f);


        float angle = Quaternion.Angle(transform.rotation, to);
        if (angle<=0)
        {

            menum = EnumInfo.MonsterState.Move;
            return;
        }

    }

    [System.Obsolete]
    public override void TracePlayer()
    {
        base.TracePlayer();

        if (mstatus.isFindPlayer == true)
        {
            Debug.Log("추적");
            agent.SetDestination(player.transform.position);
            agent.Resume();

            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.longAttackRange)
            {
                menum = EnumInfo.MonsterState.Attack;
                return;
            }

        }
        else
        {
            agent.Stop();
            menum = EnumInfo.MonsterState.Wait;
        }
    }


    public override void OnDamage()
    {
        base.OnDamage();

        mstatus.hp -= 1;
        if (mstatus.hp <= 0)
            gameObject.SetActive(false);
    }

    //이동시키는 함수
    public override void Move()
    {
        base.Move();
        if (isMoving == true)
            return;

        enemyVector = transform.position;
        StartCoroutine(Moving());
        menum = EnumInfo.MonsterState.Wait;
        isMoving = true;
    }

    IEnumerator Moving()
    {

        while (true)
        {
            transform.position =  transform.position+transform.forward * mstatus.moveSpeed * Time.deltaTime;
            if (Vector3.Distance(enemyVector, transform.position) >= xrange || mstatus.fcollision==true)
            {
                isMoving = false;
                break;
            }
            yield return null;
        }
    }

    void PlayerHpDown(int damage)
    {
        player.GetComponent<PlayerControl>().SetDamage(damage);
    }

    void IsCollision()
    {
        mstatus.fcollision=collisionFoward.GetComponent<CollisionCheck>().isCollision();
        if (mstatus.fcollision == true)
        {
            isMoving = false;
        }
    }

}

//수정예정 -> 근접시 플레이어 바라보는 걸 자연스럽게 수정