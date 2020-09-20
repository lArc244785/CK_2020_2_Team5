using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class MonsterControl : MonoBehaviour
{
    EnumInfo.MonsterState monsterState;
    public MonsterStatus mstatus;

    GameObject player;
    public NavMeshAgent nav;
    Vector3 monsvec;

    public GameObject monsterBullet;
    public Transform firePos;

    [Range(0, 30)]
    public float attackRange = 3f; //몬스터 status로 대체
    public float cooldownTime = 15f; //임시 제작
    float coolTime; //몬스터 status로 대체
    float monsterhp = 3f; //임시 hp - 몬스터 status로 대체

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monsterState = EnumInfo.MonsterState.Move;

        coolTime = 0f;
    }

    void Update()
    {
        switch (monsterState)
        {
            case EnumInfo.MonsterState.Move:
                break;

            case EnumInfo.MonsterState.Attack:
                if (mstatus.isLongMonster)
                {
                    LongMonsterAttack();
                }
                else
                    ShortMonsterAttack();
                break;

            case EnumInfo.MonsterState.CoolDown:
                break;

            default:
                break;
        }
        if (Input.GetKeyDown(KeyCode.V))
            LongMonsterAttack();
    }

    private void FixedUpdate()
    {
        MonsterTurn();
        if (mstatus.isShortMonster)
            ShortMonsterMove();
        else
            LongMonsterMove();
    }

    void MonsterTurn()
    {
        if (monsterState == EnumInfo.MonsterState.Move)
        {
            monsvec = player.transform.position - transform.position;
            transform.forward = monsvec.normalized*2;
        }
    }

    //움직임 함수에서 공격 틱 계산
    void ShortMonsterMove()
    {
        mstatus.tick += Time.deltaTime;

        if (Vector3.Distance(player.transform.position, transform.position) >= mstatus.shortMonsterRange)
        {
            nav.SetDestination(player.transform.position);
        }
        if (mstatus.tick >= mstatus.tickRate)
            monsterState = EnumInfo.MonsterState.Attack;

    }


    void LongMonsterMove()
    {
        mstatus.tick += Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) >= mstatus.longMonsterRange)
        {
            nav.SetDestination(player.transform.position);
        }
        if (mstatus.tick >= mstatus.tickRate)
            monsterState = EnumInfo.MonsterState.Attack;

    }

    void ShortMonsterAttack()
    {
        if (!mstatus.isShortMonster)
            return;

        if(Vector3.Distance(player.transform.position, transform.position) <= mstatus.shortAttackRange)
        {
            Debug.Log("단거리 공격");
            PlayerHpDown((int)mstatus.shortdamage);
        }
        mstatus.tick = 0;
        monsterState = EnumInfo.MonsterState.Move;
    }

    void LongMonsterAttack()
    {
        if (!mstatus.isLongMonster)
            return;

        Instantiate(monsterBullet, firePos.transform.position, firePos.transform.rotation);
        mstatus.tick = 0;
        monsterState = EnumInfo.MonsterState.Move;
    }

    //임시로 세번 맞으면 없어짐
    public void HpDown()
    {
        monsterhp -= 1f;
        if (monsterhp <= 0)
            gameObject.SetActive(false);
    }

    void PlayerHpDown(int damage)
    {
        player.GetComponent<PlayerControl>().SetDamage(damage);
    }
}
