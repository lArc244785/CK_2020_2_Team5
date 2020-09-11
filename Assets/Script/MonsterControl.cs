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
    Transform mtransform;

    [Range(0,30)]
    public float monsterSpeed = 3f;
    [Range(0, 30)]
    public float attackRange = 1f;
    public float cooldownTime = 1f; //임시 제작

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mtransform = GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        monsterState = MonsterState.Move;
    }

    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Move:
                MonsterMove();
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

    void MonsterMove()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= attackRange)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            monsterState = MonsterState.Attack;
        }
    }

    void MonsterAttack()
    {
        UnityEngine.Debug.Log("공격!");

        monsterState = MonsterState.CoolDown;
    }

    void MonsterCollDown()
    {
        UnityEngine.Debug.Log("쉬는중!");
        //**임시제작** 사용할 일이 있으면 사용, 필요없으면 제거

        monsterState = MonsterState.Move;
    }
}
