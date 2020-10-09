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

    //임시값들
    int rotationnum;

    //============애니메이션===================
    public Animator shortAnim;
    //=========================================

    void Start()
    {
        menum = EnumInfo.MonsterState.RandomMove;
        player = GameObject.FindGameObjectWithTag("Player");
        isMoving = false;
        agent = GetComponent<NavMeshAgent>();
        shortAnim = GetComponent<Animator>();
        shortAnim.SetBool("Idle", true);
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
        mstatus.tick += Time.deltaTime;
        if (mstatus.tick >= mstatus.tickRate)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortAttackRange)
            {
                Debug.Log("단거리 공격");

                PlayerHpDown((int)mstatus.shortdamage);
                //menum = EnumInfo.MonsterState.Trace;
                mstatus.tick = 0;
            }
            else
            {
                menum = EnumInfo.MonsterState.Move;
                //Debug.Log("사거리 부족");
            }
        }
        else
        {
            menum = EnumInfo.MonsterState.Move;
            //shortAnim.SetBool("Idle", true);
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

    public override void FindPlayer()
    {
        base.FindPlayer();

        if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortMonsterRange)// || menum !=EnumInfo.MonsterState.Attack)
        {
            mstatus.isFindPlayer = true;
            menum = EnumInfo.MonsterState.Move;


        }
        else
        {

            mstatus.isFindPlayer = false;
            agent.Stop();
        }
    }

    public override void Wait()
    {
        base.Wait();
        if (isMoving == true)
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
        Debug.Log("랜덤생성");
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
        if (angle <= 1)
        {

            Debug.Log("끝임");

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
            //shortAnim.SetTrigger("run");
            if (Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortAttackRange)
            {
                menum = EnumInfo.MonsterState.Attack;
                return;
            }
            else
            {
                //Debug.Log("놓침");
                //mstatus.isFindPlayer = false;
                //menum = EnumInfo.MonsterState.Wait;
                //agent.Stop();
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
        //shortAnim.SetTrigger("hit");
        if (mstatus.hp <= 0)
        {
            //shortAnim.SetTrigger("dead");
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
        menum = EnumInfo.MonsterState.Wait;
        isMoving = true;
    }

    IEnumerator Moving()
    {

        while (true)
        {

            transform.position = transform.position + transform.forward * mstatus.moveSpeed * Time.deltaTime;

            if (Vector3.Distance(enemyVector, transform.position) >= xrange || mstatus.fcollision == true)
            {
                isMoving = false;
                Debug.Log("움직임끝남");

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
        if (mstatus.fcollision == true)
        {
            isMoving = false;
        }
    }

}

//수정예정 -> 근접시 플레이어 바라보는 걸 자연스럽게 수정