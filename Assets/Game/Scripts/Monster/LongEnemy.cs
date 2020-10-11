using System.Collections;
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
    bool isTrace;
    bool Co_move_running = false;
    bool endAttack = true;

    //임시값들
    int rotationnum;

    //============애니메이션===================

    public Animator longAnim;

    //=========================================

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isMoving = false;
        longAnim = GetComponent<Animator>();
        menum = EnumInfo.MonsterState.RandomMove;
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

        if (!isTrace)
            return;
        if (!mstatus.isFindPlayer)
            return;

        mstatus.tick += Time.deltaTime;
        if (mstatus.tick >= mstatus.tickRate)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.longAttackRange)
            {
                Debug.Log("long거리 공격");
                if (Vector3.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
                {
                    longAnim.SetTrigger("attack");
                    endAttack = false;
                    Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
                }
                menum = EnumInfo.MonsterState.Move;
                mstatus.tick = 0;
            }

        }
        else
        {
            menum = EnumInfo.MonsterState.Move;
        }


    }

    //find가 true일 경우 처음 호출자는 부르지 말고 멀어졌나를 확인하는 코드만 호출
    public override void FindPlayer()
    {
        base.FindPlayer();

        if (!mstatus.isFindPlayer)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.longMonsterRange)// || menum !=EnumInfo.MonsterState.Attack)
            {
                StartTrace();
                menum = EnumInfo.MonsterState.Move;
                mstatus.isFindPlayer = true;

            }
        }

        if(mstatus.isFindPlayer==true && Vector3.Distance(player.transform.position, transform.position) > mstatus.longMonsterRange)
        {
            mstatus.isFindPlayer = false;
            isTrace = false;
            agent.Stop();
            menum = EnumInfo.MonsterState.RandomMove;
        }
    }

    void MonsterTurn()
    {
        if (mstatus.isFindPlayer==true && Vector3.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
        {
            //longAnim.SetTrigger("idle");
            monsvec = player.transform.position - transform.position;
            transform.forward = monsvec.normalized;

        }
    }

    public override void Wait()
    {
        base.Wait();
        if (isMoving == true)
            return;
        if (Co_move_running == true)
            return;

        Debug.Log("waiting");
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

        if (angle<=0.5)
        {
            menum = EnumInfo.MonsterState.Move;
            return;
        }

    }

    public void StartTrace()
    {
        if (!isTrace)
        {
            longAnim.SetTrigger("run");
            isTrace = true;
        }
    }

    [System.Obsolete]
    public override void TracePlayer()
    {
        base.TracePlayer();

        if (mstatus.isFindPlayer == true)
        {
            if (longAnim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                if (longAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                {
                    endAttack = true;
                    if (Vector3.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
                    {
                        if (!longAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                        {
                            Debug.Log("대기중이 아니여서 대기 세팅");
                            longAnim.SetTrigger("idle");
                        }
                    }
                    else
                    {
                        if (!longAnim.GetCurrentAnimatorStateInfo(0).IsName("run"))
                            longAnim.SetTrigger("run");
                    }
                }
            }

            else
            {
                if(!longAnim.GetCurrentAnimatorStateInfo(0).IsName("run") && !longAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    longAnim.SetTrigger("run");
            }

            agent.SetDestination(player.transform.position);
            agent.Resume();

            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.longAttackRange)
            {
                if(endAttack==true)
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
        longAnim.SetTrigger("hit");
        if (mstatus.hp <= 0)
        {
            longAnim.SetTrigger("dead");
            gameObject.SetActive(false);
        }
    }

    //이동시키는 함수
    public override void Move()
    {
        base.Move();
        if (isMoving == true)
            return;

        enemyVector = transform.position;

        StartCoroutine(Moving());
        isMoving = true;
    }

    IEnumerator Moving()
    {
        Co_move_running = true;
        longAnim.SetTrigger("walk");

        while (true)
        {
            if (!longAnim.GetCurrentAnimatorStateInfo(0).IsName("walk2"))
                longAnim.SetTrigger("walk");
            transform.position =  transform.position+transform.forward * mstatus.moveSpeed * Time.deltaTime;

            if (Vector3.Distance(enemyVector, transform.position) >= xrange || mstatus.fcollision==true)
            {
                isMoving = false;
                menum = EnumInfo.MonsterState.Wait;
                longAnim.SetTrigger("idle");
                Co_move_running = false;
                break;
            }
            yield return null;
        }
    }

    void PlayerHpDown(int damage)
    {
        //player.GetComponent<PlayerControl>().SetDamage(damage);
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

