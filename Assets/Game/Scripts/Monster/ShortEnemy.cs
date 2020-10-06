using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShortEnemy : EnemyBase
{
    public NavMeshAgent agent;
    EnumInfo.MonsterState menum;
    GameObject player;
    public GameObject collisionFoward;
    Quaternion to;

    float xrange = 0f;

    float wait = 0f;

    Vector3 enemyVector;

    bool isMoving;

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
                Attack();
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

        FindPlayer();
        IsCollision();

    }

    private void FixedUpdate()
    {
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
        if (mstatus.tick >= mstatus.tickRate)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortAttackRange)
            {
                Debug.Log("단거리 공격");
                PlayerHpDown((int)mstatus.shortdamage);
                mstatus.tick = 0;
            }
            else
            {
                menum = EnumInfo.MonsterState.Trace;
                Debug.Log("사거리 부족");
            }
        }

    }

    public override void FindPlayer()
    {
        base.FindPlayer();

        if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortMonsterRange)// || menum !=EnumInfo.MonsterState.Attack)
        {
            menum = EnumInfo.MonsterState.Move;
            mstatus.isFindPlayer = true;
            //Debug.Log("플레이어 찾음");
        }
    }

    public override void Wait()
    {
        base.Wait();

        wait += Time.deltaTime;

        if (wait >= mstatus.waitingTime)
        {
            //Debug.Log("기다림 끝");
            menum = EnumInfo.MonsterState.RandomMove;
            wait = 0;
        }
        //wait로 전환된 경우 기다림-이곳에서 계산
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
        //Debug.Log(rotationnum + " 돌리기 실행@@");

        xrange = Random.Range(mstatus.minMoveRange, mstatus.maxMoveRange);
        menum = EnumInfo.MonsterState.Turn;

    }

    void TurnEnemy()
    {

        to.eulerAngles = new Vector3(0, rotationnum, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, to, Time.deltaTime * 8f);

        float angle = Quaternion.Angle(transform.rotation, to);
        if (angle <= 0)
        {
            //move로 전환?
            menum = EnumInfo.MonsterState.Move;
            return;
        }
    }

    public override void TracePlayer()
    {
        base.TracePlayer();
        //nav를 이용해서 추적 만약 사거리를 벗어나면->랜덤으로 전환
        if (mstatus.isFindPlayer == true)
        {
            Debug.Log("추적");
            agent.SetDestination(player.transform.position);
            agent.Resume();
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortAttackRange)
                menum = EnumInfo.MonsterState.Attack;
            else
            {
                Debug.Log("놓침");
                mstatus.isFindPlayer = false;
                menum = EnumInfo.MonsterState.Wait;
                agent.Stop();
            }
        }
        else
        {
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
        //Debug.Log("움직임!!");
        enemyVector = transform.position;
        StartCoroutine(Moving());
        menum = EnumInfo.MonsterState.Wait;
        isMoving = true;
    }

    IEnumerator Moving()
    {
        //거리만 구해서 이동해야할 거리<이동한거리 가 되면 return되게끔 변경
        while (true)
        {
            transform.Translate(Vector3.forward * mstatus.moveSpeed * Time.deltaTime);

            if (Vector3.Distance(enemyVector, transform.position) >= xrange || mstatus.fcollision == true)
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
        mstatus.fcollision = collisionFoward.GetComponent<CollisionCheck>().isCollision();
    }

}

