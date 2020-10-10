using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShortEnemy : EnemyBase
{
    public NavMeshAgent agent;
    public EnumInfo.MonsterState menum;
    GameObject player;
    public GameObject collisionFoward;
    Quaternion to;
    Vector3 monsvec;
    float xrange = 0f;
    
    float wait = 0f;

    Vector3 enemyVector;

    public bool isMoving;
    public bool isTrace;
    bool Co_move_running = false;
    public bool endAttack = true;

    //임시값들
    int rotationnum;

    //============애니메이션===================
    public Animator shortAnim;
    //=========================================

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isMoving = false;
        agent = GetComponent<NavMeshAgent>();
        shortAnim = GetComponent<Animator>();
        shortAnim.SetTrigger("Idle");
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
                //Attack();
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
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortAttackRange)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
                {
                    shortAnim.SetTrigger("attack");
                    endAttack = false;
                    //공격
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

    public override void FindPlayer()
    {
        base.FindPlayer();

        if (!mstatus.isFindPlayer)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortMonsterRange)// || menum !=EnumInfo.MonsterState.Attack)
            {
                StartTrace();
                menum = EnumInfo.MonsterState.Move;
                mstatus.isFindPlayer = true;

            }
        }

        if (mstatus.isFindPlayer == true && Vector3.Distance(player.transform.position, transform.position) > mstatus.shortMonsterRange)
        {
            mstatus.isFindPlayer = false;
            isTrace = false;
            agent.Stop();
            menum = EnumInfo.MonsterState.RandomMove;
        }
    }

    void MonsterTurn()
    {
        if (mstatus.isFindPlayer == true && Vector3.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
        {
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

        wait += Time.deltaTime;

        if (wait >= mstatus.waitingTime)
        {
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


        rotationnum = Random.Range(0, 361);
        enemyVector = transform.position;

        xrange = Random.Range(mstatus.minMoveRange, mstatus.maxMoveRange);

        menum = EnumInfo.MonsterState.Turn;

    }

    void TurnEnemy()
    {

        to.eulerAngles = new Vector3(0, rotationnum, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, to, Time.deltaTime * 8f);

        float angle = Quaternion.Angle(transform.rotation, to);

        if (angle <= 0.5)
        {

            menum = EnumInfo.MonsterState.Move;
            return;
        }
    }

    public void StartTrace()
    {
        if (!isTrace)
        {
            shortAnim.SetTrigger("run");
            isTrace = true;
        }
    }


    [System.Obsolete]
    public override void TracePlayer()
    {
        base.TracePlayer();

        if (mstatus.isFindPlayer == true)
        {
            if (shortAnim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                if (shortAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                {
                    endAttack = true;
                    if (Vector3.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
                    {
                        if (!shortAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                            shortAnim.SetTrigger("idle");
                    }
                    else {
                        if(!shortAnim.GetCurrentAnimatorStateInfo(0).IsName("run"))
                            shortAnim.SetTrigger("run");
                    }
                }
            }

            else
            {
                if (!shortAnim.GetCurrentAnimatorStateInfo(0).IsName("run") && !shortAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                {
                    shortAnim.SetTrigger("run");
                }
            }

            agent.SetDestination(player.transform.position);
            agent.Resume();

            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortAttackRange)
            {
                if (endAttack == true)
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
        shortAnim.SetTrigger("hit");
        if (mstatus.hp <= 0)
        {
            shortAnim.SetTrigger("dead");
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
        shortAnim.SetTrigger("walk");
        while (true)
        {
            transform.position = transform.position + transform.forward * mstatus.moveSpeed * Time.deltaTime;

            if (Vector3.Distance(enemyVector, transform.position) >= xrange || mstatus.fcollision == true)
            {
                isMoving = false;
                menum = EnumInfo.MonsterState.Wait;
                shortAnim.SetTrigger("idle");
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
        mstatus.fcollision = collisionFoward.GetComponent<CollisionCheck>().isCollision();
        if (mstatus.fcollision == true)
        {
            isMoving = false;
        }
    }

}

//수정예정 -> 근접시 플레이어 바라보는 걸 자연스럽게 수정