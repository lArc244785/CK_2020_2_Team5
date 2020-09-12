using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum MonsterState
{
    Move=1,
    Attack,
    CoolDown //임시 제작
}

public class MonsterControl : MonoBehaviour
{
    MonsterState monsterState;
    GameObject player;
    NavMeshAgent nav;

    Vector3 monsvec;

    [Range(0, 30)]
    public float attackRange = 3f;
    public float cooldownTime = 15f; //임시 제작
    float coolTime;

    float monsterhp = 3f; //임시 hp

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        monsterState = MonsterState.Move;

        coolTime = 0f;
    }

    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Move:
                break;

            case MonsterState.Attack:
                MonsterAttack();
                break;

            case MonsterState.CoolDown:
                MonsterCollDown();
                break;

            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        MonsterTurn();
        MonsterMove();
    }

    void MonsterTurn()
    {
        if (monsterState == MonsterState.Move)
        {
            monsvec = player.transform.position - transform.position;
            transform.forward = monsvec.normalized;
        }
    }

    void MonsterMove()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= attackRange)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            //monsterState = MonsterState.Attack;
        }
    }

    void MonsterAttack()
    {
        //UnityEngine.Debug.Log("공격!");

        //monsterState = MonsterState.CoolDown;
    }

    void MonsterCollDown()
    {
        //UnityEngine.Debug.Log("쉬는중!");
        //**임시제작** 사용할 일이 있으면 사용, 필요없으면 제거
        //if (coolTime >= cooldownTime)
        //{
        //    coolTime = 0f;
        //    monsterState = MonsterState.Move;
        //}
        //else
        //    coolTime += Time.deltaTime;
    }

    //임시로 세번 맞으면 없어짐
    public void HpDown()
    {
        monsterhp -= 1f;
        if (monsterhp <= 0)
            gameObject.SetActive(false);
    }
}
